using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using papuff.datainfra.ORM;
using papuff.domain.Core.Companies;
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

                    var pass = "123456".Encrypt();

                    var u0001 = new User("lucas@papuff.com", pass, "lucas.moraes");
                    var u0002 = new User("joao@papuff.com", pass, "joao.silva");
                    var u0003 = new User("carla@papuff.com", pass, "carla.ramos");
                    var u0004 = new User("suport@papuff.com", pass, "suport.papuff");
                    var u0005 = new User("financial@papuff.com", pass, "financial.papuff");
                    var u9999 = new User("root@papuff.com", pass, "root.papuff");

                    u0001.SetType(UserType.Customer);
                    u0002.SetType(UserType.Customer);
                    u0003.SetType(UserType.Customer);
                    u0004.SetType(UserType.Operator);
                    u0005.SetType(UserType.Enterprise);
                    u9999.SetType(UserType.Root);
                    context.Users.AddRange(u0001, u0002, u0003, u0004, u0005, u9999);

                    var c0001 = new Company("Papuff LTDA.", "papuff@gmail.com", "www.papuff.com.br", "0000000000", "0000000000", "0000000000", DateTime.Now, u0005.Id);
                    context.Companies.Add(c0001);

                    var g_u0001 = new General(new DateTime(1994, 9, 22), "Lucas Moraes", "some description", CurrentStage.Aproved, u0001.Id);
                    var g_u0002 = new General(new DateTime(1994, 9, 22), "Joao da Silva", "some description", CurrentStage.Blocked, u0002.Id);
                    var g_u0003 = new General(new DateTime(1994, 9, 22), "Carla Ramos", "some description", CurrentStage.Pending, u0003.Id);
                    var g_u0004 = new General(new DateTime(1994, 9, 22), "Suporte Papuff", "Suporte Ninja do Papuff", CurrentStage.Aproved, u0004.Id);
                    var g_u0005 = new General(new DateTime(1994, 9, 22), "Financeiro Papuff", "Financeiro Ninja do Papuff", CurrentStage.Aproved, u0005.Id);
                    var g_u9999 = new General(new DateTime(1994, 9, 22), "Ninja das Sombras", "Sou o GM da porra toda", CurrentStage.Aproved, u9999.Id);
                    context.Generals.AddRange(g_u0001, g_u0002, g_u0003, g_u0004, g_u0005, g_u9999);

                    var a_u0001 = new Address(BuildingType.Townhouse, 56, 0, "Rua Carlos Albino", "Guabirutuba", "Curitiba", "PR", "Brasil", "00000000", u0001.Id, false);
                    var a_u0002 = new Address(BuildingType.Commercial, 11, 1, "Rua Padre Germano", "Centro", "Curitiba", "PR", "Brasil", "00000000", u0002.Id, false);
                    var a_u0003 = new Address(BuildingType.House, 241, 0, "Rua Roberto Lobo", "Moçungue", "Curitiba", "PR", "Brasil", "00000000", u0003.Id, false);
                    var a_u0004 = new Address(BuildingType.Commercial, 000, 0, "Papuff Street", "Papuff", "Papuff", "PP", "System", "00000000", u0004.Id, false);
                    var a_u0005 = new Address(BuildingType.Commercial, 000, 0, "Papuff Street", "Papuff", "Papuff", "PP", "System", "00000000", u0005.Id, false);
                    var a_u0006 = new Address(BuildingType.Commercial, 000, 0, "Papuff Street", "Papuff", "Papuff", "PP", "System", "00000000", c0001.Id, true);
                    var a_u9999 = new Address(BuildingType.Commercial, 000, 0, "Vale Encantado", "Paradise", "Grin Wood", "WZ", "System", "00000000", u9999.Id, false);
                    context.Addresses.AddRange(a_u0001, a_u0002, a_u0003, a_u0004, a_u0005, a_u0006, a_u9999);

                    //var w_u0001 = new Wallet(PaymentType.Debit, "0000", "0000", "00000000", 15, true, u0001.Id);
                    //var w_u0002 = new Wallet(PaymentType.Credit, "0000", "0000", "00000000", 15, true, u0002.Id);
                    //var w_u0003 = new Wallet(PaymentType.Uninformed, "0000", "0000", "00000000", 15, true, u0003.Id);
                    //var w_u0004 = new Wallet(PaymentType.Uninformed, "0000", "0000", "00000000", 0, true, u0004.Id);
                    //var w_u0005 = new Wallet(PaymentType.Uninformed, "0000", "0000", "00000000", 0, true, u0005.Id);
                    //var w_u9999 = new Wallet(PaymentType.Uninformed, "0000", "0000", "00000000", 0, true, u9999.Id);
                    //context.Wallets.AddRange(w_u0001, w_u0002, w_u0003, w_u0004, w_u0005, w_u9999);

                    var d_u0001 = new Document("0000", "www.google.com.br", DocumentType.RG, u0001.Id);
                    var d_u0002 = new Document("0000", "www.google.com.br", DocumentType.CPF, u0002.Id);
                    var d_u0003 = new Document("0000", "www.google.com.br", DocumentType.CNH, u0003.Id);
                    var d_u0004 = new Document("0000", "www.google.com.br", DocumentType.CNH, u0004.Id);
                    var d_u0005 = new Document("0000", "www.google.com.br", DocumentType.CNH, u0005.Id);
                    var d_u9999 = new Document("0000", "www.google.com.br", DocumentType.CNH, u9999.Id);
                    context.Documents.AddRange(d_u0001, d_u0002, d_u0003, d_u0004, d_u0005, d_u9999);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}