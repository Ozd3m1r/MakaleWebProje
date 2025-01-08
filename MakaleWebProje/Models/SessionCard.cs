using Entities.Models;
using MailChimp.Net.Models;
using MakaleWebProje.Extensions;
using Newtonsoft.Json;  


namespace MakaleWebProje.Models
{
    public class SessionCard:Card
    {
        [JsonIgnore]
        public ISession? Session { get; set; }

        public static Card GetCard(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()
                .HttpContext?.Session;

            SessionCard card = session?.GetJson<SessionCard>("card")
                ?? new SessionCard();
            card.Session = session;
            return card;
        }

        public override void AddItem(Makale makae, int sayı)
        {
            base.AddItem(makae, sayı);
            Session?.Setjson<SessionCard>("card", this);
        }
        public override void Clear()
        {
            base.Clear();
            Session?.Remove("card");
        }
        public override void RemoveLine(Makale makale)
        {
            base.RemoveLine(makale);
            Session?.Setjson<SessionCard>("card", this);
        }
    }
}
