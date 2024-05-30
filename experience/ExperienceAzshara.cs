using OWOGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using System.Timers;
using System.Collections;
using System.Security.Policy;

namespace OwoAdvancedSensationBuilder.experience {
    internal class ExperienceAzshara {

        Dictionary<double, List<AdvancedSensationStreamInstance>> queue = new Dictionary<double, List<AdvancedSensationStreamInstance>>();
        int count = 0;

        public Dictionary<double, List<AdvancedSensationStreamInstance>> getAzshara() {
            List<string> prio = AdvancedSensationManager.getInstance().priorityList;
            prio.Add("boomingWave");

            queue = new Dictionary<double, List<AdvancedSensationStreamInstance>>();
            count = 0;

            add(5, getWindRising());
            add(23, getBoomingWave());
            return queue;
        }

        private AdvancedSensationStreamInstance getWindRising() {
            Sensation windRising1 = SensationsFactory.Create(100, 5, 12, 2, 0, 0).WithMuscles(Muscle.All);

            Sensation windConstant2 = SensationsFactory.Create(90, 4f, 12, 0, 0, 0).WithMuscles(Muscle.All);
            Sensation windRising2 = SensationsFactory.Create(90, 4f, 17, 2, 0, 0).WithMuscles(Muscle.All);

            Sensation windConstant3 = SensationsFactory.Create(80, 3.5f, 17, 0, 0, 0).WithMuscles(Muscle.All);
            Sensation windRising3 = SensationsFactory.Create(80, 3.5f, 25, 2, 0, 0).WithMuscles(Muscle.All);

            Sensation windConstant4 = SensationsFactory.Create(70, 3.5f, 25, 0, 0, 0).WithMuscles(Muscle.All);
            Sensation windRising4 = SensationsFactory.Create(70, 3.5f, 35, 2, 0, 0).WithMuscles(Muscle.All);

            AdvancedSensationBuilderMergeOptions options = new AdvancedSensationBuilderMergeOptions();
            Sensation windBaseline = new AdvancedSensationBuilder(windRising1)
                .appendNow(windRising2, windConstant2)
                .appendNow(windRising3, windConstant3)
                .appendNow(windRising4, windConstant4)
                .getSensationForStream();

            return new AdvancedSensationStreamInstance("windRising", windBaseline);
        }

        private AdvancedSensationStreamInstance getBoomingWave() {
            Sensation boomingWave = SensationsFactory.Create(45, 3, 50, 0.2f, 0, 0).WithMuscles(Muscle.All);
            return new AdvancedSensationStreamInstance("boomingWave", boomingWave);
        }

        private string getName() {
            count++;
            return count.ToString();
        }


        private void add(double time, AdvancedSensationStreamInstance sensation) {
            if (queue.ContainsKey(time)) {
                queue[time].Add(sensation);
            } else {
                List<AdvancedSensationStreamInstance> list = new List<AdvancedSensationStreamInstance>();
                list.Add(sensation);
                queue.Add(time, list);
            }
        }
    }
}
