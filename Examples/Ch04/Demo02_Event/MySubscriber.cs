using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo02_Event
{
    public class MySubscriber
    {
        private MyPublisher _publisher;

        public MySubscriber()
        {
            _publisher = new MyPublisher();
            _publisher.TextChanged += this.TextChangedHandler; // 訂閱事件
        }

        public void TextChangedHandler(string oldValue, string newValue)
        {
            Console.WriteLine("Text 屬性已經變成：{1}", oldValue, newValue); // 5
        }

        public void SetText()
        {
            _publisher.Text = "Hello, event!"; // 這行程式碼會觸發事件
        }
    }
}
