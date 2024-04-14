namespace Project.Enums
{
    public enum RegionalCity
    {
        Благо̀евград,
        Бурга̀с,
        Ва̀рна,
        ВелѝкоТъ̀рново,
        Вѝдин,
        Вра̀ца,
        Га̀брово,
        До̀брич,
        Къ̀рджали,
        Кюстендѝл,
        Ло̀веч,
        Монта̀на,
        Па̀зарджик,
        Пѐрник,
        Плѐвен,
        Пло̀вдив,
        Ра̀зград,
        Ру̀се,
        Силѝстра,
        Слѝвен,
        Смо̀лян,
        Сòфия,
        Ста̀раЗаго̀ра,
        Търго̀вище,
        Ха̀сково,
        Шу̀мен,
        Я̀мбол
    }

    public static class RegionalCityExtensions
    {
        public static string ToFormattedString(this RegionalCity city)
        {
            
            return city.ToString();
        }
    }
}
