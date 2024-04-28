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


    internal class AdvancedSensationInstance {

        public delegate void SensationInstanceEvent(AdvancedSensationInstance instance);
        
        public event SensationInstanceEvent LastCalculationOfCycle;

        public string name { get; }
        public int firstTick { get; set; }

        private List<SensationWithMuscles> _sensations;
        public List<SensationWithMuscles> sensations { get { return _sensations; } }

        public AdvancedSensationInstance(string name, Sensation sensation = null) {
            this.name = name;
            firstTick = 0;
            if (sensation != null) {
                _sensations = new AdvancedSensationBuilder(sensation).getSnippets();
            }
        }

        public Sensation getSensationAtTick(int tick) {
            if (sensations.Count == 0) {
                return null;
            }

            int playedSensation = (tick - firstTick) % sensations.Count;

            if (sensations.Count - 1 == playedSensation) {
                // trigger events for the last calculation
                LastCalculationOfCycle?.Invoke(this);
            }

            return sensations[playedSensation];
        }

        public void updateSensation(List<SensationWithMuscles> newSensations) {
            _sensations = newSensations;
        }

    }
}
