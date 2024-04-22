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
    internal class AdvancedSensationManager {

        private static AdvancedSensationManager managerInstance;

        private System.Timers.Timer timer;
        private Dictionary<string, AdvancedSensationManagerInstance> sensations;

        int step = 0;
        bool calculating = false;
        Sensation calculatedSensation = null;

        private AdvancedSensationManager() {
            timer = new System.Timers.Timer(100);
            timer.Elapsed += streamSensation;
            timer.Elapsed += calcSensation;
            timer.AutoReset = true;
            timer.Enabled = false;

            sensations = new Dictionary<string, AdvancedSensationManagerInstance>();
        }

        public static AdvancedSensationManager getInstance() {
            if (managerInstance == null) {
                managerInstance = new AdvancedSensationManager();
            }
            return managerInstance;
        }

        private void streamSensation(Object source, ElapsedEventArgs e) {
            OWO.Send(calculatedSensation);
            step++;
            Console.WriteLine("streamSensation finish {0:HH:mm:ss.fff}", e.SignalTime);
        }

        private void calcSensation(Object source, ElapsedEventArgs e) {
            if (calculating) {
                return;
            }
            try {
                calculating = true;
                actualCalcSensation();
            } finally {
                calculating = false;
            }
        }

        private void actualCalcSensation() {
            int calcStep = step;
            AdvancedSensationBuilder builder = null;

            foreach (AdvancedSensationManagerInstance sensationInstance in sensations.Values) {
                Sensation sensationTick = sensationInstance.getSensationAtTick(calcStep);
                if (sensationTick == null) {
                    continue;
                }

                if (builder == null) {
                    builder = new AdvancedSensationBuilder(sensationTick);
                } else {
                    builder.merge(sensationTick, AdvancedSensationBuilder.MuscleMergeMode.MAX);
                }
            }
            calculatedSensation = builder.build();
            Console.WriteLine("calcSensation finish step: " + calcStep);
        }

        public void add(string name, Sensation sensation) {
            AdvancedSensationManagerInstance instance = new AdvancedSensationManagerInstance(sensation);
            sensations.Add(name, instance);
            calcSensation(null, null);
            timer.Start();
        }

        public void stopAll() {
            timer.Stop();
            sensations.Clear();
            step = 0;
        }



    }
}
