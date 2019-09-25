using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using papuff.datainfra.ORM;
using papuff.domain.Core.Companies;
using papuff.domain.Core.Enums;
using papuff.domain.Core.Generals;
using papuff.domain.Core.Users;
using papuff.domain.Core.Wallets;
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

                    var u_0001 = new User("lucas@papuff.com", pass, "lucas.moraes");
                    var u_0002 = new User("joao@papuff.com", pass, "joao.silva");
                    var u_0003 = new User("carla@papuff.com", pass, "carla.ramos");
                    var u_0004 = new User("suport@papuff.com", pass, "suport.papuff");
                    var u_0005 = new User("financial@papuff.com", pass, "financial.papuff");
                    var u_9999 = new User("root@papuff.com", pass, "root.papuff");

                    u_0001.SetType(UserType.Customer);
                    u_0002.SetType(UserType.Customer);
                    u_0003.SetType(UserType.Customer);
                    u_0004.SetType(UserType.Operator);
                    u_0005.SetType(UserType.Enterprise);
                    u_9999.SetType(UserType.Root);
                    context.Users.AddRange(u_0001, u_0002, u_0003, u_0004, u_0005, u_9999);

                    var c_0001 = new Company("Papuff LTDA.", "papuff@gmail.com", "www.papuff.com.br", "0000000000", "0000000000", "0000000000", DateTime.Now, u_0005.Id);
                    context.Companies.Add(c_0001);

                    var g_0001 = new General(new DateTime(1994, 9, 22), "Lucas Moraes", "some description", CurrentStage.Aproved, u_0001.Id);
                    var g_0002 = new General(new DateTime(1994, 9, 22), "Joao da Silva", "some description", CurrentStage.Blocked, u_0002.Id);
                    var g_0003 = new General(new DateTime(1994, 9, 22), "Carla Ramos", "some description", CurrentStage.Pending, u_0003.Id);
                    var g_0004 = new General(new DateTime(1994, 9, 22), "Suporte Papuff", "Suporte Ninja do Papuff", CurrentStage.Aproved, u_0004.Id);
                    var g_0005 = new General(new DateTime(1994, 9, 22), "Financeiro Papuff", "Financeiro Ninja do Papuff", CurrentStage.Aproved, u_0005.Id);
                    var g_9999 = new General(new DateTime(1994, 9, 22), "Ninja das Sombras", "Sou o GM da porra toda", CurrentStage.Aproved, u_9999.Id);
                    context.Generals.AddRange(g_0001, g_0002, g_0003, g_0004, g_0005, g_9999);

                    var a_0001 = new Address(BuildingType.Townhouse, 56, 0, "Rua Carlos Albino", "Guabirutuba", "Curitiba", "PR", "Brasil", "00000000", u_0001.Id, false);
                    var a_0002 = new Address(BuildingType.Commercial, 11, 1, "Rua Padre Germano", "Centro", "Curitiba", "PR", "Brasil", "00000000", u_0002.Id, false);
                    var a_0003 = new Address(BuildingType.House, 241, 0, "Rua Roberto Lobo", "Moçungue", "Curitiba", "PR", "Brasil", "00000000", u_0003.Id, false);
                    var a_0004 = new Address(BuildingType.Commercial, 000, 0, "Papuff Street", "Papuff", "Papuff", "PP", "System", "00000000", u_0004.Id, false);
                    var a_0005 = new Address(BuildingType.Commercial, 000, 0, "Papuff Street", "Papuff", "Papuff", "PP", "System", "00000000", u_0005.Id, false);
                    var a_0006 = new Address(BuildingType.Commercial, 000, 0, "Papuff Street", "Papuff", "Papuff", "PP", "System", "00000000", c_0001.Id, true);
                    var a_9999 = new Address(BuildingType.Commercial, 000, 0, "Vale Encantado", "Paradise", "Grin Wood", "WZ", "System", "00000000", u_9999.Id, false);
                    context.Addresses.AddRange(a_0001, a_0002, a_0003, a_0004, a_0005, a_0006, a_9999);

                    var w_0001 = new Wallet(u_0001.Id);
                    var w_0002 = new Wallet(u_0002.Id);
                    var w_0003 = new Wallet(u_0003.Id);
                    var w_0004 = new Wallet(u_0004.Id);
                    var w_0005 = new Wallet(u_0005.Id);
                    var w_9999 = new Wallet(u_9999.Id);
                    context.Wallets.AddRange(w_0001, w_0002, w_0003, w_0004, w_0005, w_9999);

                    var r_0001 = new Receipt("0000", "0000", 10, w_0001.Id);
                    var r_0002 = new Receipt("0000", "0000", 10, w_0002.Id);
                    var r_0003 = new Receipt("0000", "0000", 10, w_0003.Id);
                    var r_0004 = new Receipt("0000", "0000", 10, w_0004.Id);
                    var r_0005 = new Receipt("0000", "0000", 10, w_0005.Id);
                    var r_9999 = new Receipt("0000", "0000", 10, w_9999.Id);
                    context.Receipts.AddRange(r_0001, r_0002, r_0003, r_0004, r_0005, r_9999);

                    var p_0001 = new Payment("00000000", DateTime.Now.AddYears(2), 234, 15, "00000000", true, PaymentType.Credit, w_0001.Id);
                    var p_0002 = new Payment("00000000", DateTime.Now.AddYears(2), 234, 15, "00000000", true, PaymentType.Credit, w_0002.Id);
                    var p_0003 = new Payment("00000000", DateTime.Now.AddYears(2), 234, 15, "00000000", true, PaymentType.Credit, w_0003.Id);
                    var p_0004 = new Payment("00000000", DateTime.Now.AddYears(2), 234, 15, "00000000", true, PaymentType.Credit, w_0004.Id);
                    var p_0005 = new Payment("00000000", DateTime.Now.AddYears(2), 234, 15, "00000000", true, PaymentType.Credit, w_0005.Id);
                    var p_9999 = new Payment("00000000", DateTime.Now.AddYears(2), 234, 15, "00000000", true, PaymentType.Credit, w_9999.Id);
                    context.Payments.AddRange(p_0001, p_0002, p_0003, p_0004, p_0005, p_9999);

                    var d_0001 = new Document("0000", "www.google.com.br", DocumentType.RG, u_0001.Id);
                    var d_0002 = new Document("0000", "www.google.com.br", DocumentType.CPF, u_0002.Id);
                    var d_0003 = new Document("0000", "www.google.com.br", DocumentType.CNH, u_0003.Id);
                    var d_0004 = new Document("0000", "www.google.com.br", DocumentType.CNH, u_0004.Id);
                    var d_0005 = new Document("0000", "www.google.com.br", DocumentType.CNH, u_0005.Id);
                    var d_9999 = new Document("0000", "www.google.com.br", DocumentType.CNH, u_9999.Id);
                    context.Documents.AddRange(d_0001, d_0002, d_0003, d_0004, d_0005, d_9999);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}