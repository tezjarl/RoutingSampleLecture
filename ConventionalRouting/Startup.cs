using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConventionalRouting
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			// Включение механизма маршрутизации
			app.UseRouting();
			// Описание сопоставления конкретных адресов с конкретными обработчиками
			app.UseEndpoints(endpoints =>
			{
				// Сопоставление по умолчанию, корневой путь "/" и простейший обработчик
				endpoints.MapGet("/", async context =>
				{
					await context.Response.WriteAsync("Hello world!");
				});
				//Более сложный шаблон
				// /test/{name} - адрес с обязательной частью /test и переменной частью, сохраняемой в параметре name 
				// :alpha - ограничение на параметр name, только латинские буквы
				// :minlength(4) - еще одно ограничение на параметр name, длина не менее 4 символов
				// =Mike - значение по умолчанию для параметра name, которое используется если ничего не передано
				endpoints.MapGet("/test/{name:alpha:minlength(4)=Mike}",async context =>
				{
					var name = context.Request.RouteValues["name"];
					await context.Response.WriteAsync($"Hello {name} for test!");
				});

			});
		}
	}
}