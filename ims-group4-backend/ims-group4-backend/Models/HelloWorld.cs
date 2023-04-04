
namespace ims_group4_backend.Models
{
    public class HelloWorld
    {
        private const string eng = "Hello World!";
        private const string frc = "Bonjour le monde!!";
        private const string mal = "Hai dunia!!";
        private const string man = "你好，世界！";
        private const string swe = "Hej världen!";
        private string[] m_helloWorldArr = new string[] {eng, frc, mal, man, swe};

        public string? Get(int id)
        {
            if(m_helloWorldArr.Length < id+1)
            {
                return null;
            } 
            else
            {
                return m_helloWorldArr[id];
            }
        }

        public string Set(string value)
        {
            m_helloWorldArr.Append(value);
            
            return m_helloWorldArr.Last();
        }
    }
}
