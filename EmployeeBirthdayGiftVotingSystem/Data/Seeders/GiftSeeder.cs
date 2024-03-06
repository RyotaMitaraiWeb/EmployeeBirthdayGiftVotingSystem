using EmployeeBirthdayGiftVotingSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeBirthdayGiftVotingSystem.Data.Seeders
{
    public class GiftSeeder : IEntityTypeConfiguration<Gift>
    {
        public void Configure(EntityTypeBuilder<Gift> builder)
        {
            builder.HasData(
                    new Gift()
                    {
                        Id = 1,
                        Name = "Joycons",
                        Description = "A pair of joycons for the Nintendo Switch. The joycons will be bought brand new, so hopefully they won't drift.",
                        ImageFileName = "joycons.jpg",
                    },
                    new Gift()
                    {
                        Id = 2,
                        Name = "Pizza cutter wheel",
                        Description = "A high-quality pizza cutter wheel, made of Swedish steel.",
                        ImageFileName = "pizzacutter.jpg",
                    },
                    new Gift()
                    {
                        Id = 3,
                        Name = "\"I hate Mondays\" mug",
                        Description = "A mug with text that reveals how everyone really feels.",
                        ImageFileName = "mug.jpg",
                    },
                    new Gift()
                    {
                        Id = 4,
                        Name = "Pyraminx",
                        Description = "What people would describe as a \"Rubik Pyramid\".",
                        ImageFileName = "pyraminx.jpg",
                    },
                    new Gift()
                    {
                        Id = 5,
                        Name = "Fake Java license",
                        Description = "A license for the new and totally existant and non-fake Java implementation, which can be executed inside the browser.",
                        ImageFileName = "fake-javascript-license.png",
                    },
                    new Gift()
                    {
                        Id = 6,
                        Name = "Magic Wand",
                        Description = "A wand that grants any wish (when those wishes will be granted remains to be seen).",
                        ImageFileName = "magic-wand.png",
                    }
                );
        }
    }
}
