using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://yusshctp:C0imaO4kz0g7P98gi13n_RLbrAq1okiV@fish.rmq.cloudamqp.com/yusshctp");


using var connection = factory.CreateConnection();

var channel = connection.CreateModel();

//durable : false olursa kuyruklar memory'de tutulur. rabbitmq restart atarsa memory'de gidecegi icin memory'deki datalar da gider. durable : true yaparsak diskte tutulur. 
//exclusive : true olursa burdaki kuyruga sadece burada olusturdugum kanal uzerinden ulasabiliriz. bizim istedigimiz burdaki kuyruga subscriber tarafında farklı bir kanal uzerinden baglanacagım icin exclusive : false yapmam gerek.
//autoDelete : eger bu kuyruga baglı olan son subscriber da baglantısını koparırsa kuyrugu otomatik siler. bu istedigimiz bisey degil
//rabbitmq ya mesajlarımızı byte[] olarak gondeririz.boyle oldugu icin istedigimiz herseyi gonderebiliriz. pdf,image,buyuk bir dosya da gonderebiliriz. Onun icin gonderecegimiz seyi byte[] dizisine dondurelim

channel.QueueDeclare("hello-queue",true,false,false);

string message = "hello-world";

var messageBody = Encoding.UTF8.GetBytes(message);

//exchange kullanmadıgım, direkt kuyruga gonderdigim icin string.Empty ile bos geciyoruz..arada exchange kullanmadan direkt kuyruga gonderirsek buna default exchange deriz. eger ki default exchange kullanıyorusak route key'imize kuyruktaki ismi vermemiz gerekiyor.
channel.BasicPublish(string.Empty, "hello-queue", null, messageBody);

Console.WriteLine("mesaj gonderildi"); 

Console.ReadLine();
