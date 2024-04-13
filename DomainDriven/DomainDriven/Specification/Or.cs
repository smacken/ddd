using System.Linq.Expressions;

namespace DomainDriven.Specification
{
    public class Or<T> : Specification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public Or(ISpecification<T> left, ISpecification<T> right)
        {
            this._left = left;
            this._right = right;
        }

        public override Expression<Func<T, bool>> SpecExpression
        {
            get
            {
                ParameterExpression? parameter = Expression.Parameter(typeof(T), "obj");
                Expression<Func<T, bool>>? expression = Expression.Lambda<Func<T, bool>>(
                    Expression.OrElse(
                        Expression.Invoke(_left.SpecExpression, parameter),
                        Expression.Invoke(_right.SpecExpression, parameter)
                    ),
                    parameter
                );
                return expression;
            }
        }
    }
}