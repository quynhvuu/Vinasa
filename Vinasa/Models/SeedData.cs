using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Vinasa.Data;

namespace Vinasa.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            SeminarInitialize(serviceProvider);
            SeminarParticipantInitialize(serviceProvider);
        }
        private static void SeminarInitialize(IServiceProvider serviceProvider)
        {
            using (var context = new VinasaContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<VinasaContext>>()))
            {
                // Look for any movies.
                if (context.Seminar.Any())
                {
                    return;   // DB has been seeded
                }

                context.Seminar.AddRange(
                    new Seminar
                    {
                        Title = "Hội nghị kết nối đầu tư tổ chức tại TP. Hồ Chí Minh: chủ đề THÚC ĐẨY ĐẦU TƯ VÀ PHÁT TRIỂN HỆ SINH THÁI TÀI CHÍNH SỐ",
                        OpenDate = "9:00 - 17:00 ngày 28/06/2022 (Thứ 3)",
                        Address = "Sihub, 273 Điện Biên Phủ, Phường Võ Thị Sáu, Quận 3, TP. HCM",
                        CreatedUtc = DateTime.UtcNow
                    }
                );
                context.SaveChanges();
            }
        }
        private static void SeminarParticipantInitialize(IServiceProvider serviceProvider)
        {
            using (var context = new VinasaContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<VinasaContext>>()))
            {
                // Look for any movies.
                if (context.SeminarParticipant.Any())
                {
                    return;   // DB has been seeded
                }

                context.SeminarParticipant.AddRange(
                    new SeminarParticipant
                    {
                        TaxNumber = "0315159393",
                        SeminarId = 1,
                        Company = "Công ty Cổ phần FPT",
                        Name = "Trương Gia Bình",
                        Position = "Chủ tịch HĐQT",
                        Email = "manhnt@vinasa.org.vn",
                        PhoneNumber = "0937688958",
                        ProvinceId = 1,
                        JobTitle = "Doanh nghiệp CNTT",
                        Operation = "Công nghệ thông tin",
                        RegistrySeminar = true,
                        RegistryBusinessMatching = true,
                        RegistryExhibition = true,
                        RegistryTicket = true,
                        CreatedUtc = DateTime.UtcNow
                    }
                );
                context.SaveChanges();
            }
        }

    }
}
