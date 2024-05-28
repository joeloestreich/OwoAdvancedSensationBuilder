using OWOGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OwoAdvancedSensationBuilder.AdvancedSensationBuilder;

namespace OwoAdvancedSensationBuilder {
    internal class AdvancedSensationBuilderMergeOptions {
        public enum MuscleMergeMode { MAX, KEEP, OVERRIDE, MIN };

        public MuscleMergeMode mode { get; set; }
        public float delaySeconds { get; set; }

        public AdvancedSensationBuilderMergeOptions() {
            mode = MuscleMergeMode.MAX;
            delaySeconds = 0.0f;
        }

        public AdvancedSensationBuilderMergeOptions afterDelay(float delay) {
            delaySeconds = delaySeconds + delay;
            return this;
        }
    }
}
