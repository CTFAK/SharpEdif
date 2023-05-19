using System.Collections.Generic;

namespace SharpEdif.User
{
    public unsafe static class UserMethods
    {
        public static short CreateRunObject(LPRDATA* rdPtr, LPEDATA* edPtr, fpcob* cobPtr)
        {
            // For now, this is the only exposed method. Initialize your fields here. The constructor for RunData is being called **before** this method is called
            return 0;
        }

        public static void FillProperties(FusionProperties props)
        {
            props.Items.Add(FusionProp.CreateStatic("Test 1","The info"));
            props.Items.Add(FusionProp.CreateEditString("Test 2","The info"));
        }
    }
}