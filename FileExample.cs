using System.ComponentModel.DataAnnotations;

namespace GetGether.Models
{
    public class EventParticipant
    {

        [Key] // Определение первичного ключа
        public int EventParticipantId { get; set; } // Пример имени свойства для первичного ключа
        public int EventId { get; set; }
        public Event Event { get; set; }

        public string ProfileUserNameId { get; set; }
        public Profile Profile { get; set; }

        public string PersonStatusFromEvent { get; set; } //Статус пользователя в событии (Participant, Amdin, Owner)
    }
 public List<Event> CreateTestEvents(List<RegisterUser> possibleOrganizers)
        {
            return new List<Event>
            {
                new Event()
                {
                    EventName = "Birthday Bash",           
                    Description = "Join us for an unforgettable birthday celebration for John! An evening filled with music, laughter, and joy awaits."
                 },
                new Event()
                 {
                    EventName = "Company Retreat",
                    Description = "Get ready for our annual company retreat! A weekend of team-building activities, workshops, and relaxation."
                 },
                 new Event()
                 {
                    EventName = "Charity Fundraiser",
                    Description = "Join hands for a noble cause! Our charity fundraiser aims to make a difference in the lives of those in need."
                 },
                  new Event()
                 {
                    EventName = "Charity Fundraiser",
                    Description = "Join hands for a noble cause! Our charity fundraiser aims to make a difference in the lives of those in need."
                 },
                 new Event()
                {
                    EventName = "test",
                    Description = "Join us for a jubilant celebration honoring Spartak's triumph! Revel in an evening of camaraderie and festivity as we commemorate the victory achieved by the Spartak team. Let's come together to rejoice, reminisce, and create unforgettable memories in tribute to their success!",
                 }
            };
        }
 public async Task<bool> AddDataToDataBase()
        {
            var users = CreateTestUsers();
            var events = CreateTestEvents(users);
            var eventParticipant = BindEventsToProfiles(users, events);

            for (int i = 0; i < 5; i++) {
                var result = await _authService.RegisterUser(users[i]);
                if (!result.Succeeded) {
                    var errors = result.Errors.Select(e => e.Description);
                    throw new Exception($"При регистрации пользователя возникли ошибки: {string.Join(',', errors)}");
                }
            }
            //профили создаются неявно при регистрации пользователя
            var profiles = _dbContext.Profiles.ToList();

            for (int i = 0; i < 5; i++) {
                profiles[i].Events.Add(events[i]);
                profiles[i].EventParticipants.Add(eventParticipant[i]);
                events[i].EventParticipants.Add(eventParticipant[i]);
            }    public class RoomViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        [RegularExpression(@"^\w+( \w+)*$", ErrorMessage = "Characters allowed: letters, numbers, and one space between words.")]
        public string Name { get; set; }

        public string Admin { get; set; }
    }
}
namespace GetGether.Models
{

    /// <summary>
    /// Модель события
    /// </summary>

    public class Event : BaseModel
    {
        public string EventName { get; set; }   //Название события
        public string Description { get; set; }  //Описание события
        public int BackGroundCover { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public double Location { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public int MinParticipantsCount { get; set; }
        public int MaxParticipantCount { get; set; }

        [ForeignKey("Русский текст внутри атрибута")]
        public int? OrganizationId { get; set; }
        public Organization ? Organization { get; set; }



        //public int GenderRestriction { get; set; }

        //[ForeignKey("Organizer")]
        //public string OrganizerUserNameId { get; set; } // Внешний ключ для организатора
        //public Profile Organizer { get; set; }    // Организатор события




        public virtual ICollection<EventParticipant> EventParticipants { get; set; }

        public Event() 
        {
            EventParticipants = new List<EventParticipant>();
        }

    }
    
}
public List<Event> CreateTestEvents(List<RegisterUser> possibleOrganizers)
{
    return new List<Event>
    {
        new Event()
        {
            EventName = "День рождения",           
            Description = "Присоединяйтесь к незабыв\"аемому празднованию дня\" рождения Джона! Вас ждет вечер, наполненный музыкой, смехом и радостью."
        },
        new Event()
        {
            EventName = "Корпоративный отдых",
            Description = "Готовьтесь к нашему ежегодному корпоративному отдыху! Вас ждет уикенд с командообразующими мероприятиями, мастер-классами и отдыхом."
        },
        new Event()
        {
            EventName = "Благотворительный вечер",
            Description = "Объединимся ради благородной цели! Наш благотворительный вечер направлен на то, чтобы изменить жизнь нуждающихся к лучшему."
        },
        new Event()
        {
            EventName = "Благотворительный вечер",
            Description = "Объединимся ради благородной цели! Наш благотворительный вечер направлен на то, чтобы изменить жизнь нуждающихся к лучшему."
        },
        new Event()
        {
            EventName = "Тестовое мероприятие",
            Description = "Присоединяйтесь к радостному празднованию в честь триумфа Спартака! Проведите вечер в атмосфере товарищества и веселья, отмечая победу команды Спартак. Давайте соберемся вместе, чтобы порадоваться, вспомнить и создать незабываемые воспоминания в честь их успеха!"
        }
    };
}

/*
 * "Это многострочный комментарий".
 * Он может занимать несколько строк.
 */
int y = 10;

/// <summary>
/// Это метод для добавления двух чисел.
/// </summary>
/// <param name="a">"Первое число".</param>
/// <param name="b">Второе число.</param>
/// <returns>Сумма двух чисел.</returns>
public int Add(int a, int b)
{
    return a + b;
}
