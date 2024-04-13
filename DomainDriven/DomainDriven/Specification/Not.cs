using System.Linq.Expressions;

namespace DomainDriven.Specification
{
    public class Not<T> : Specification<T>
    {
        private readonly ISpecification<T> _inner;

        public Not(ISpecification<T> inner)
        {
            _inner = inner;
        }

        public override Expression<Func<T, bool>> SpecExpression
        {
            get
            {
                ParameterExpression? parameter = Expression.Parameter(typeof(T), "obj");
                return Expression.Lambda<Func<T, bool>>(
                    Expression.Not(
                        Expression.Invoke(_inner.SpecExpression, parameter)
                    ),
                    parameter
                );
            }
        }
    }
}