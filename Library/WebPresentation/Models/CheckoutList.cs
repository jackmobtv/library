using DataDomain;

namespace WebPresentation.Models
{
    public static class CheckoutList
    {
        public static List<Copy> copies = new List<Copy>();

        public static bool IsInList(int copyId)
        {
            foreach (var copy in copies)
            {
                if(copy.CopyID == copyId)
                {
                    return true;
                }
            }

            return false;
        }

        public static void RemoveFromList(int copyId)
        {
            for (int i = 0; i < copies.Count; i++)
            {
                if (copies[i].CopyID == copyId)
                {
                    copies.RemoveAt(i);

                    return;
                }
            }
        }
    }
}
