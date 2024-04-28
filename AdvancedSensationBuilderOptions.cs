using OWOGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OwoAdvancedSensationBuilder.AdvancedSensationBuilder;

namespace OwoAdvancedSensationBuilder {
    internal class AdvancedSensationBuilderOptions {
        public Muscle[] muscles { get; set; }
        public bool streamMode { get; set; }

        public AdvancedSensationBuilderOptions() {
            muscles = null;
            streamMode = false;
        }


    public AdvancedSensationBuilderOptions copyWithoutMuscles() {
            AdvancedSensationBuilderOptions copy = new AdvancedSensationBuilderOptions();
            copy.muscles = null;
            copy.streamMode = streamMode;
            return copy;
        }
    }
}
