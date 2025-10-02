using My_Web.Interfaces;
using My_Web.Models;

public class CartService : ICartService
{
    private readonly AppDbContext _context;

    public CartService(AppDbContext context)
    {
        _context = context;
    }

    public List<CartItem> GetCart(int userId)
    {
        return _context.CartItems
                       .Where(c => c.UserID == userId)
                       .ToList();
    }

    public bool AddItem(int userId, int productId)
    {
        var cartItem = _context.CartItems
                               .FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);

        if (cartItem == null)
        {
            _context.CartItems.Add(new CartItem
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 1
            });
        }
        else
        {
            cartItem.Quantity++;
            _context.CartItems.Update(cartItem);
        }

        _context.SaveChanges();
        return true;
    }

    public bool RemoveItem(int userId, int productId)
    {
        var cartItem = _context.CartItems
                               .FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);

        if (cartItem == null) return false;

        _context.CartItems.Remove(cartItem);
        _context.SaveChanges();
        return true;
    }
}
