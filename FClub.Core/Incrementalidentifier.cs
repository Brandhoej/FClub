using System;

namespace FClub.Core
{
    public class Incrementalidentifier : IIdentifier
    {
        private int m_counter = 0;

        public int GetNextId()
        {
            if (m_counter == int.MaxValue)
            {
                throw new OverflowException("Coutner overflow");
            }

            return m_counter++;
        }
    }
}
