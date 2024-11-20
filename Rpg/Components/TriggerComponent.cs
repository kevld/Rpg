using Microsoft.Xna.Framework;
using Rpg.Core.Components;
using Rpg.Models;
using System;

namespace Rpg.Components
{
    public class TriggerComponent : BaseComponent
    {
        public bool HasBeenEnabled { get; private set; }
        private Entity SpiedEntity { get; set; }
        private Rectangle CoveredArea { get; set; }

        public event EventHandler OnTriggerEvent;

        public TriggerComponent(Entity owner, Rectangle coveredArea) : base(owner)
        {
            CoveredArea = coveredArea;
            HasBeenEnabled = false;
        }

        public bool AddSpy(Entity spied)
        {
            if (spied == null)
                return false;

            SpiedEntity = spied;
            return true;
        }

        public override void Update(GameTime gameTime)
        {
            if (SpiedEntity == null || HasBeenEnabled)
                return;

            if (ShouldTrigger())
                Enable();
        }

        public void ResetTrigger(bool revokeSpy)
        {
            HasBeenEnabled = false;
            if (revokeSpy)
                SpiedEntity = null;
        }

        private void Enable()
        {
            if (HasBeenEnabled)
                return;

            HasBeenEnabled = true;
            OnTriggerEvent?.Invoke(this, EventArgs.Empty);
        }

        private bool ShouldTrigger()
        {
            if (CoveredArea.Intersects(SpiedEntity.GetRectangle()))
                return true;
            return false;
        }
    }
}
