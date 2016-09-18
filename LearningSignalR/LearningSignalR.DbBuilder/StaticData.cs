using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using LearningSignalR.Db;
using LearningSignalR.Db.Models;
using LearningSignalR.Db.Models.Identity;
using LearningSignalR.DbBuilder.Properties;
using LearningSignalR.Identity.Managers;
using LearningSignalR.Identity.Stores;
using Microsoft.AspNet.Identity;

namespace LearningSignalR.DbBuilder
{
    public static class StaticData
    {
        public static void Seed(AppDbContext dbContext)
        {
            var companyPeekClopenburg = new Company
            {
                Name = "Peek & Clopenburg"
            };

            var companyZara = new Company
            {
                Name = "Zara"
            };

            dbContext.Companies.AddOrUpdate(x => x.Name, companyPeekClopenburg, companyZara);
            dbContext.SaveChanges();

//            var user = new User
//            {
//                Email = DbConstants.DefaultAdministratorUser.Email,
//                UserName = DbConstants.DefaultAdministratorUser.Email,
//                LastName = DbConstants.DefaultAdministratorUser.LastName,
//                FirstName = DbConstants.DefaultAdministratorUser.FirstName,
//                PhoneNumber = DbConstants.DefaultAdministratorUser.PhoneNo,
//                AccessFailedCount = 0,
//                EmailConfirmed = true,
//                IsDisabled = false,
//                LockoutEnabled = true,
//                TwoFactorEnabled = false,
//                PhoneNumberConfirmed = true,
//            };

            var dummyUsers = new[]
            {
                new User
                {
                    Email = "alexandru.zainescu@peeknclopenburg.ro",
                    UserName = "alexandru.zainescu@peeknclopenburg.ro",
                    LastName = "Zainescu",
                    FirstName = "Alexandru",
                    AccessFailedCount = 0,
                    EmailConfirmed = true,
                    IsDisabled = false,
                    LockoutEnabled = true,
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = true,
                    CompanyId = companyPeekClopenburg.Id
                },
                new User
                {
                    Email = "vlad.dinu@peeknclopenburg.ro",
                    UserName = "vlad.dinu@peeknclopenburg.ro",
                    LastName = "Dinu",
                    FirstName = "Vlad",
                    AccessFailedCount = 0,
                    EmailConfirmed = true,
                    IsDisabled = false,
                    LockoutEnabled = true,
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = true,
                    CompanyId = companyPeekClopenburg.Id
                },
                new User
                {
                    Email = "bogdan.balazsfi@peeknclopenburg.ro",
                    UserName = "bogdan.balazsfi@peeknclopenburg.ro",
                    LastName = "Balazsfi",
                    FirstName = "Alexandru",
                    AccessFailedCount = 0,
                    EmailConfirmed = true,
                    IsDisabled = false,
                    LockoutEnabled = true,
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = true,
                    CompanyId = companyPeekClopenburg.Id
                },
                new User
                {
                    Email = "alexandru.purdila@zara.ro",
                    UserName = "alexandru.purdila@zara.ro",
                    LastName = "Purdila",
                    FirstName = "Alexandru",
                    AccessFailedCount = 0,
                    EmailConfirmed = true,
                    IsDisabled = false,
                    LockoutEnabled = true,
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = true,
                    CompanyId = companyZara.Id
                },
                new User
                {
                    Email = "mihai.ivanciu@zara.ro",
                    UserName = "mihai.ivanciu@zara.ro",
                    LastName = "Ivanciu",
                    FirstName = "Mihai",
                    AccessFailedCount = 0,
                    EmailConfirmed = true,
                    IsDisabled = false,
                    LockoutEnabled = true,
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = true,
                    CompanyId = companyZara.Id
                },
                new User
                {
                    Email = "alin.zdurlan@zara.ro",
                    UserName = "alin.zdurlan@zara.ro",
                    LastName = "Zdurlan",
                    FirstName = "Alin",
                    AccessFailedCount = 0,
                    EmailConfirmed = true,
                    IsDisabled = false,
                    LockoutEnabled = true,
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = true,
                    CompanyId = companyZara.Id
                },
            };

            using (var roleManager = new AppRoleManager(new AppRoleStore(dbContext) { DisposeContext = false }))
            {
                foreach (var roleName in DbConstants.Roles.All.Select(x => x.Name))
                {
                    if (roleManager.RoleExists(roleName) == false)
                        roleManager.Create(new Role { Name = roleName });
                }
            }
            dbContext.SaveChanges();

            var userManagerArgs = new AppUserManagerArgs
            {
                UserLockoutEnabledByDefault = Settings.Default.UserLockoutEnabledByDefault,
                PasswordRequireDigit = Settings.Default.PasswordRequireDigit,
                PasswordRequireLowercase = Settings.Default.PasswordRequireLowercase,
                PasswordRequireNonLetterOrDigit = Settings.Default.PasswordRequireNonLetterOrDigit,
                PasswordRequireUppercase = Settings.Default.PasswordRequireUppercase,
                PasswordRequiredLength = Settings.Default.PasswordRequiredLength,
                UserAccountLockoutMinutes = Settings.Default.UserAccountLockoutMinutes,
                UserAllowOnlyAlphanumericUserNames = Settings.Default.UserAllowOnlyAlphanumericUserNames,
                UserMaxFailedAccessAttemptsBeforeLockout = Settings.Default.UserMaxFailedAccessAttemptsBeforeLockout,
                UserRequireUniqueEmail = Settings.Default.UserRequireUniqueEmail
            };

            using (var userManager = new AppUserManager(new AppUserStore(dbContext) { DisposeContext = false }, userManagerArgs))
            {
//                if (userManager.FindByEmail(user.Email) == null)
//                {
//                    userManager.Create(user, DbConstants.DefaultAdministratorUser.Password);
//                    userManager.AddToRole(user.Id, DbConstants.Roles.Administrator.Name);
//                }

                foreach (var dummyUser in dummyUsers)
                    if (userManager.FindByEmail(dummyUser.Email) == null)
                    {
                        userManager.Create(dummyUser, DbConstants.DefaultAdministratorUser.Password);
                        userManager.AddToRole(dummyUser.Id, DbConstants.Roles.Client.Name);
                    }
            }
            dbContext.SaveChanges();

            var userIds = dummyUsers.Select(x => x.Id).ToArray();
            var random = new Random();

            var messages = new List<Message>();
            for (var index = 0; index < 25; index++)
            {
                var randomInteger = random.Next(0, 100);
                var companyId = randomInteger%2 == 0 ? companyPeekClopenburg.Id : companyZara.Id;
                var sentByUserId = userIds[random.Next(0, userIds.Length)];
                var sentAt = DateTime.UtcNow.AddSeconds(-1*random.Next(0, 432000));

                var body = Faker.Lorem.Paragraph();
                if (body.Length > DbConstants.StringLength4096)
                    body = body.Substring(0, DbConstants.StringLength4096 - 4) + " ...";

                var subject = Faker.Lorem.Sentence();
                if (subject.Length > DbConstants.StringLength)
                    subject = subject.Substring(0, DbConstants.StringLength - 4) + " ...";

                messages.Add(new Message
                {
                    Body = body,
                    Subject = subject,
                    IsRead = false,
                    CompanyId = companyId,
                    SentByUserId = sentByUserId,
                    SentAt = sentAt
                });
            }
            dbContext.Messages.AddRange(messages);
            dbContext.SaveChanges();
        }

    }
}