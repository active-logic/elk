public static class StringExt{

    public static bool ContainsChar(this string self, char c){
        #if UNITY_2021_OR_LATER
            return self.Contains(c);
        #else
            foreach(var x in self) if(x == c) return true;
            return false;
        #endif
    }

}
