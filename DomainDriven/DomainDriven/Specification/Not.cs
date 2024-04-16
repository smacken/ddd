namespace DomainDriven.Specification
{
    using System.Linq.Expressions;

    public class Not<T> : Specification<T>
    {
        private readonly ISpecification<T> inner;

        public Not(ISpecification<T> inner)
        {
            this.inner = inner;
        }

        /// <inheritdoc/>
        public override Expression<Func<T, bool>> SpecExpression
        {
            get
            {
                ParameterExpression? parameter = Expression.Parameter(typeof(T), "obj");
                return Expression.Lambda<Func<T, bool>>(
                    Expression.Not(Expression.Invoke(this.inner.SpecExpression, parameter)),
                    parameter
                );
            }
        }
    }
}
