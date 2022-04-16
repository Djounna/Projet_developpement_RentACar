namespace ErrorManagement
{
    public class CustomError : ApplicationException
    {
        private int _ErrorId;
        private string _ErrorMessage;
        public CustomError(int pErrorId)
        {


            switch (pErrorId)
            {
                case 0: break;
            }
        }


    }
}