namespace BookStore.Shared.Utils
{
    public class Either
    {
        private readonly object L;
        private readonly object R;

        public Either(object L, object R) 
        {
            this.L = L;
            this.R = R;
        }

        public bool IsRight() 
        {
            return L == null;
        }

        public bool IsLeft()
        {
            return R == null;
        }

        public T Value<T>()
        {
           return (T)R ?? (T)L;
        }

        public static Either Right(object r) 
        {
            return new Either(null, r);
        }

        public static Either Left(object l)
        {
            return new Either(l, null);
        }
    }
    public class Either<T, S> where T : class where S : class
    {
        private readonly T L;
        private readonly S R;

        private Either(T L, S R)
        {
            this.L = L;
            this.R = R;
        }

        public S GetRight()
        {
            return R;
        }

        public T GetLeft()
        {
            return L;
        }

        public bool IsRight()
        {
            return L == null;
        }

        public bool IsLeft()
        {
            return R == null;
        }

        public static Either<T, S> Right(S r)
        {
            return new Either<T, S>(null, r);
        }

        public static Either<T, S> Left(T l)
        {
            return new Either<T, S>(l, null);
        }
    }
}
