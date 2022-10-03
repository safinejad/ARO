using System.Security.Cryptography;
using Contracts.Annotations;

namespace Contracts.DataModel
{
    public class CancellationPolicy//OneToManyRoom
    {
        public long Id { get; set; }
        public long RoomId { get; set; }
        public HotelRoomPrice Room { get; set; }
        public int? DayBeforeCheckOut { get; set; }
        public TimeSpan? TimeBeforeCheckOut { get; set; }
        [Money]
        public decimal? CashCharge { get; set; }
        [Money]
        public decimal? PercentCharge { get; set; }
    }







}