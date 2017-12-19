

using BEPUutilities;
namespace BEPUphysics.BroadPhaseSystems
{
    ///<summary>
    /// Requires that a class have a BoundingBox.
    ///</summary>
    public interface IBoundingBoxOwner
    {
        ///<summary>
        /// Gets the bounding capsule of the object.
        ///</summary>
        BoundingBox BoundingBox { get; }
    }
}
