using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo03_EventArgs
{
    public class MySubscriber
    {
        private MyPublisher _publisher;

        public MySubscriber()
        {
            _publisher = new MyPublisher();
            _publisher.TextChanged += this.TextChangedHandler; // 訂閱事件
        }

        public void TextChangedHandler(object sender, TextChangedEventArgs args)
        {
            Console.WriteLine("Text 屬性已經變成：{1}", args.OldValue, args.NewValue); 
        }

        public void SetText()
        {
            _publisher.Text = "Hello, event!"; // 這行程式碼會造成事件觸發
        }
    }
}
