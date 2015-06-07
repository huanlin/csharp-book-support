using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo01_PublisherSubscriber
{
    public class MySubscriber
    {
        private MyPublisher _publisher;

        public MySubscriber()
        {
            _publisher = new MyPublisher();
            _publisher.TextChanged += this.TextChangedHandler; 
        }

        public void TextChangedHandler(string oldValue, string newValue)
        {
            Console.WriteLine("Text 屬性已經變成：{1}", oldValue, newValue); // 5
        }

        public void SetText()
        {
            _publisher.Text = "Hello, event!"; // 這行程式碼會造成事件觸發 4
        }
    }
}
