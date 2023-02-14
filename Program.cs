namespace AspRoutingDependsApp
{
    public class TimeService
    {
        public string Time => DateTime.Now.ToLongTimeString();
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddTransient<TimeService>();

            var app = builder.Build();

            //app.Map("/time", (TimeService service) => $"Time now: {service.Time}");
            //app.Map("/{controller}/index", (string? controller) => $"Controller: {controller}");
            //app.Map("/type/{action}", (string? action) => $"Action: {action}");
            //app.Map("/time", GetTime);

            app.Map("/", () =>
            {
                Console.WriteLine("Index endpoint start");
                return "Home page";
            });

            app.Map("/user", () =>
            {
                Console.WriteLine("User endpoint start");
                return "User page";
            });



            app.Use(async (context, next) =>
            {
                //await context.Response.WriteAsync("First middleware start");
                await next.Invoke();
                await context.Response.WriteAsync("First middleware finish");
            });

            app.Use(async (context, next) =>
            {
                Console.WriteLine("Second middleware start");
                await next.Invoke();
                Console.WriteLine("Second middleware finish");
            });

            //app.Run(async context => 
            //{
            //    await context.Response.WriteAsync("Terminal component!");
            //});
            

            app.Run();
        }

        static string GetTime(TimeService service)
        {
            return $"Time now: {service.Time}";
        }
    }
}