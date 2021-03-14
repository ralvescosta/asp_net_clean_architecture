namespace BookStore.Shared.Utils
{
    public abstract class Either<TLeft, TRight>
    {
        public abstract bool IsRight();
        public abstract bool IsLeft();
        public abstract TRight GetRight();
        public abstract TLeft GetLeft();
    }

    public class Left<TLeft, TRight> : Either<TLeft, TRight>
    {
        TLeft Value { get; }
        public Left(TLeft value)
        {
            this.Value = value;
        }
        public override bool IsRight() => false;
        public override bool IsLeft() => true;
        public override TLeft GetLeft() => this.Value;
        public override TRight GetRight() => default;
    }

    public class Right<TLeft, TRight> : Either<TLeft, TRight>
    {
        TRight Value { get; }
        public Right(TRight value)
        {
            this.Value = value;
        }
        public override bool IsRight() => true;
        public override bool IsLeft() => false;
        public override TLeft GetLeft() => default;
        public override TRight GetRight() => this.Value;
    }
}
