using System.Linq.Expressions;

namespace DomainDriven.Specification
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> SpecExpression { get; }
        bool IsSatisfiedBy(T obj);
    }
    
    public abstract class Specification<T> : ISpecification<T>
    {
        private Func<T, bool>? _compiledExpression;

        private Func<T, bool> CompiledExpression =>
            _compiledExpression ?? (_compiledExpression = SpecExpression.Compile());

        public abstract Expression<Func<T, bool>> SpecExpression { get; }

        public bool IsSatisfiedBy(T obj) => CompiledExpression.Invoke(obj);
    }

    public static class SpecExtensions
    {
        public static ISpecification<T> And<T>(this ISpecification<T> left, ISpecification<T> right) 
            => new And<T>(left, right);

        public static ISpecification<T> Or<T>(this ISpecification<T> left, ISpecification<T> right) 
            => new Or<T>(left, right);

        public static ISpecification<T> Not<T>(this ISpecification<T> inner)
            => new Not<T>(inner);
    }
}