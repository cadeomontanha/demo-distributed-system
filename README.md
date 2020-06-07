# Demo Distributed System

Esse projeto tem o intuito de demonstrar sistemas distribidos 

## O que � o Memcached
O Memcached � um banco de dados chave e valor de c�digo aberto e alto desempenho, de mem�ria distribu�da, destinado a acelerar aplica��es Web, aliviando a carga do banco de dados.

Para mais informa��es consultar o site: https://memcached.org/

## Docker
Para o exemplo foram usadas as seguintes imagens:

`docker run --name mariadb -e MYSQL_ROOT_PASSWORD=SUA_SENHA -d -p 3306:3306 mariadb`

`docker run --name rabbitmq -d -p 5672:5672 -p 15672:15672 --hostname rabbitmq rabbitmq:3-management`

`docker run -p 11211:11211 -d memcached`

## Packages
A aplica��o faz uso do NuGet package `EnyimMemcachedCore`
https://www.nuget.org/packages/EnyimMemcachedCore/


### Rodar a aplica��o

**WARNING**
Caso n�o seja definido a configura��o abaixo no appSettings, a aplica��o ir� ser configurada para rodar localmente.

```json
  "MemcachedSettings": {
    "Port": 11211,
    "Address": "localhost"
  }
```


```csharp
    public static class MemcachedConfigurator
    {
        public static IServiceCollection AddMemcached(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEnyimMemcached(o => o.Servers = new List<Server>
            {
                CreateServerConfig(services)
            });

            services.AddSingleton<ICacheProvider, CacheProvider>();
            services.AddSingleton<ICacheRepository, CacheRepository>();

            return services;
        }

        private static Server CreateServerConfig(IServiceCollection services)
        {
            const int PORT_DEFAULT = 11211;
            const string ADDRESS_DEFAULT = "localhost";

            var sp = services.BuildServiceProvider();
            IOptions<MemcachedSettings> memcachedSettings = sp.GetService<IOptions<MemcachedSettings>>();

            if (memcachedSettings.Value is null)
                return new Server { Address = ADDRESS_DEFAULT, Port = PORT_DEFAULT };

            return new Server { Address = memcachedSettings.Value.Address, Port = memcachedSettings.Value.Port };
        }
    }
```
