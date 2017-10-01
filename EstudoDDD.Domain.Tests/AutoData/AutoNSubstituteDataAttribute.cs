using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Xunit2;

namespace EstudoDDD.Domain.Tests.AutoData
{
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute()
            : base(new Fixture()
                .Customize(new AutoNSubstituteCustomization()))
        {
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}