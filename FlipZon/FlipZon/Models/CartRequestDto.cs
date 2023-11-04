using System;
using SQLite;

namespace FlipZon.Models
{
    public class CartRequestDto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

