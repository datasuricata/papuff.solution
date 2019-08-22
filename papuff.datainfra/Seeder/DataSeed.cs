using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using papuff.datainfra.ORM;
using papuff.domain.Core.Enums;
using papuff.domain.Core.Generals;
using papuff.domain.Core.Users;
using papuff.domain.Security;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace papuff.datainfra.Seeder {
    public static class DataSeed {
        public static async Task InitializeAsync(IServiceProvider service) {

            using (var context = new AppDbContext(service.GetRequiredService<DbContextOptions<AppDbContext>>())) {

                if (!context.Users.Any()) {

                    var pass = "123456";

                    var lucas = new User("lucas@papuff.com", pass.Encrypt(), "lucas.moraes");
                    lucas.SetType(UserType.Customer);

                    var joao = new User("joao@papuff.com", pass.Encrypt(), "joao.silva");
                    joao.SetType(UserType.Customer);

                    var carla = new User("carla@papuff.com", pass.Encrypt(), "carla.ramos");
                    carla.SetType(UserType.Customer);

                    var suport = new User("suport@papuff.com", pass.Encrypt(), "suport.papuff");
                    suport.SetType(UserType.Operator);

                    var root = new User("root@papuff.com", pass.Encrypt(), "suport.papuff");
                    root.SetType(UserType.Root);

                    context.Users.AddRange(lucas, joao, carla, suport, root);

                    var g_lucas = new General(new DateTime(1994, 9, 22), "Lucas Moraes", "some description", CurrentStage.Aproved, lucas.Id);
                    var g_joao = new General(new DateTime(1994, 9, 22), "Joao da Silva", "some description", CurrentStage.Blocked, joao.Id);
                    var g_carla = new General(new DateTime(1994, 9, 22), "Carla Ramos", "some description", CurrentStage.Pending, carla.Id);
                    var g_suport = new General(new DateTime(1994, 9, 22), "Suporte Papuff", "Suporte Ninja do Papuff", CurrentStage.Aproved, suport.Id);
                    var g_root = new General(new DateTime(1994, 9, 22), "Ninja das Sombras", "Sou o GM da porra toda", CurrentStage.Aproved, root.Id);

                    context.Generals.AddRange(g_lucas, g_joao, g_carla, g_suport, g_root);

                    var a_lucas = new Address(BuildingType.Townhouse, 56, 0, "Rua Carlos Albino", "Guabirutuba", "Curitiba", "PR", "Brasil", "00000000", lucas.Id);
                    var a_joao = new Address(BuildingType.Commercial, 11, 1, "Rua Padre Germano", "Centro", "Curitiba", "PR", "Brasil", "00000000", joao.Id);
                    var a_carla = new Address(BuildingType.House, 241, 0, "Rua Roberto Lobo", "Moçungue", "Curitiba", "PR", "Brasil", "00000000", carla.Id);
                    var a_suport = new Address(BuildingType.Commercial, 000, 0, "Virtual Street", "Internet", "Curitiba", "PR", "Brasil", "00000000", suport.Id);
                    var a_root = new Address(BuildingType.Commercial, 000, 0, "Sombras do Valle", "Moçungue", "Curitiba", "PR", "Brasil", "00000000", root.Id);

                    context.Addresses.AddRange(a_lucas, a_joao, a_carla, a_suport, a_root);

                    var w_lucas = new Wallet(PaymentType.Debit, "0000", "0000", "00000000", 15, true, lucas.Id);
                    var w_joao = new Wallet(PaymentType.Credit, "0000", "0000", "00000000", 15, true, joao.Id);
                    var w_carla = new Wallet(PaymentType.Uninformed, "0000", "0000", "00000000", 15, true, carla.Id);

                    context.Wallets.AddRange(w_lucas, w_joao, w_carla);

                    var d_lucas = new Document("0000", "www.google.com.br", DocumentType.RG, lucas.Id);
                    var d_joao = new Document("0000", "www.google.com.br", DocumentType.CPF, joao.Id);
                    var d_carla = new Document("0000", "www.google.com.br", DocumentType.CNH, carla.Id);

                    context.Documents.AddRange(d_lucas, d_joao, d_carla);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
