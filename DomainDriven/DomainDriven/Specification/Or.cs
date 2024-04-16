namespace DomainDriven.Specification
{
    using System.Linq.Expressions;

    public class Or<T> : Specification<T>
    {
        private readonly ISpecification<T> left;
        private readonly ISpecification<T> right;

        public Or(ISpecification<T> left, ISpecification<T> right)
        {
            this.left = left;
            this.right = right;
        }

        /// <inheritdoc/>
        public override Expression<Func<T, bool>> SpecExpression
        {
            get
            {
                ParameterExpression? parameter = Expression.Parameter(typeof(T), "obj");
                Expression<Func<T, bool>>? expression = Expression.Lambda<Func<T, bool>>(
                    Expression.OrElse(
                        Expression.Invoke(this.left.SpecExpression, parameter),
                        Expression.Invoke(this.right.SpecExpression, parameter)
                    ),
                    parameter
                );
                return expression;
            }
        }
    }
}
