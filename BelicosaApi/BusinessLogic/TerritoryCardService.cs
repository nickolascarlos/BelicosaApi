using BelicosaApi.Exceptions;
using BelicosaApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BelicosaApi.BusinessLogic
{
    public class TerritoryCardService : ContextServiceBase<BelicosaApiContext>
    {
        public TerritoryCardService(BelicosaApiContext context) : base(context)
        {
        }

        public async Task<TerritoryCard?> Get(int territoryCardId)
        {
            return await _context.TerritoryCard
                .Include(card => card.Territory)
                    .ThenInclude(territory => territory.CanAttack)
                .Include(card => card.Holder)
                    .ThenInclude(player => player!.User)
                .Include(card => card.Game)
                .Include(card => card.Holder)
                .FirstOrDefaultAsync(card => card.Id == territoryCardId);
        }

        public async Task<List<TerritoryCard>> GetAll(int gameId)
        {
            return _context.TerritoryCard
                .Include(card => card.Territory)
                    .ThenInclude(territory => territory.CanAttack)
                .Include(card => card.Holder)
                    .ThenInclude(player => player!.User)
                .Where(card => card.Game.Id == gameId)
                .ToList();
        }

        public async Task<List<TerritoryCard>> GetAll(BelicosaGame game)
        {
            return await GetAll(game.Id);
        }

        public async Task<List<TerritoryCard>> GetFromPlayer(int playerId)
        {
            return _context.TerritoryCard
                .Include(card => card.Territory)
                .Where(card => (card.Holder != null) &&  card.Holder.Id == playerId)
                .ToList();
        }

        public async Task<List<TerritoryCard>> GetFromPlayer(Player player)
        {
            return await GetFromPlayer(player.Id);
        }

        public async Task ExchangeCards(BelicosaGame game, List<TerritoryCard> cards, Player player)
        {
            if (cards.Count != 3)
            {
                throw new ArgumentException("Exactly 3 cards are needed for an exchange");
            }

            if (cards.Any(card => card.Holder is null) || cards.Any(card => card.Holder!.Id != player.Id))
            {
                throw new CardNotOwnedByPlayerException();
            }

            if (cards.Any(card => card.Holder!.GameId != game.Id))
            {
                throw new CardNotBelongingToGameException();
            }

            bool exchangeable = AllCardsHaveDifferentShapes(cards) || AllCardsHaveTheSameShape(cards);

            if (!exchangeable)
            {
                throw new Exception("Cards are not exchangeable");
            }

            foreach (TerritoryCard card in cards)
            {
                card.Holder = null;
                _context.Update(card);
            }

            await _context.SaveChangesAsync();

            game.CardExchangeCount += 1;
            // TODO: Replace this formula
            player.AvailableFreeDistributionTroops = game.CardExchangeCount * 5;

            _context.UpdateRange([game, player]);
            await _context.SaveChangesAsync();
        }

        private bool AllCardsHaveTheSameShape(List<TerritoryCard> cards)
        {
            return cards.TrueForAll(card => card.Shape == cards[0].Shape);
        }

        private bool AllCardsHaveDifferentShapes(List<TerritoryCard> cards)
        {
            // TODO: Improve this code
            for (int i = 0; i < 3; i++)
            {
                TerritoryCard card = cards[i];
                for (int j = 0; j < i; j++)
                {
                    if (card.Shape == cards[j].Shape)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
