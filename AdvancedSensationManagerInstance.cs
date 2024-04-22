using OWOGame;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace OwoAdvancedSensationBuilder {
    internal class AdvancedSensationManagerInstance {

        List<SensationWithMuscles> sensations;

        public AdvancedSensationManagerInstance(Sensation sensation) {
            sensations = new AdvancedSensationBuilder(sensation).getSnippets();
        }

        public Sensation getSensationAtTick(int tick) {
            if (sensations.Count == 0) {
                return null;
            }
            return sensations[tick % sensations.Count];
        }

    }
}
