using OWOGame;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static OwoAdvancedSensationBuilder.AdvancedSensationBuilderMergeOptions;

namespace OwoAdvancedSensationBuilder {
    internal class AdvancedSensationManager {

        private enum ProcessState { ADD, REMOVE, UPDATE }

        private static AdvancedSensationManager managerInstance;

        private System.Timers.Timer timer;

        private Dictionary<string, AdvancedSensationInstance> playSensations;
        private Dictionary<AdvancedSensationInstance, ProcessState> processSensation;

        int tick = 0;
        bool calculating = false;
        Sensation calculatedSensation = null;

        private AdvancedSensationManager() {
            timer = new System.Timers.Timer(100);
            timer.Elapsed += streamSensation;
            timer.Elapsed += calcManagerTick;
            timer.AutoReset = true;
            timer.Enabled = false;

            playSensations = new Dictionary<string, AdvancedSensationInstance>();
            processSensation = new Dictionary<AdvancedSensationInstance, ProcessState>();
        }

        public static AdvancedSensationManager getInstance() {
            if (managerInstance == null) {
                managerInstance = new AdvancedSensationManager();
            }
            return managerInstance;
        }

        private void streamSensation(Object source, ElapsedEventArgs e) {
            OWO.Send(calculatedSensation);
            tick++;
        }

        private void calcManagerTick(Object source, ElapsedEventArgs e) {
            if (calculating) {
                return;
            }
            try {
                calculating = true;
                processRemove(false);
                processUpdate();
                processAdd();
                calcSensation();
                processRemove(true);
            } finally {
                calculating = false;
            }
        }

        private void processUpdate() {
            foreach (var process in new Dictionary<AdvancedSensationInstance, ProcessState>(processSensation)) {

                if (process.Value != ProcessState.UPDATE) {
                    continue;
                }
                AdvancedSensationInstance instance = process.Key;
                AdvancedSensationInstance oldInstance = null;

                if (playSensations.Keys.Contains(instance.name)) {
                    // Update Playing Sensation
                    oldInstance = playSensations[instance.name];
                } else {
                    foreach (var processInstance in processSensation) {
                        if (processInstance.Value == ProcessState.ADD && processInstance.Key.name == instance.name) {
                            // Update to be Added Sensation
                            oldInstance = processInstance.Key;
                            break;
                        }
                    }
                }

                if (oldInstance != null) {
                    oldInstance.updateSensation(instance.origSensation);
                }

                processSensation.Remove(process.Key);

            }
        }

        private void processAdd() {
            foreach (var process in new Dictionary<AdvancedSensationInstance, ProcessState>(processSensation)) {

                if (process.Value != ProcessState.ADD) {
                    continue;
                }
                AdvancedSensationInstance instance = process.Key;
                instance.firstTick = tick;

                playSensations[instance.name] = instance;

                processSensation.Remove(process.Key);
            }
        }

        private void processRemove(bool endOfCylce) {
            foreach (var process in new Dictionary<AdvancedSensationInstance, ProcessState>(processSensation)) {

                if (process.Value != ProcessState.REMOVE) {
                    continue;
                }

                AdvancedSensationInstance instance = process.Key;

                if (playSensations.ContainsKey(instance.name)) {
                    playSensations.Remove(instance.name);
                }

                processSensation.Remove(process.Key);
            }

            if (playSensations.Count == 0 && endOfCylce) {
                // Only allow to stop manager at end of cycle.
                // Else race time conditions might stop manager while something to add just got inserted.
                bool toAdd = false;
                foreach (var processInstance in processSensation) {
                    if (processInstance.Value == ProcessState.ADD) {
                        toAdd = true;
                        break;
                    }
                }
                if (!toAdd) {
                    resetManagerState();
                }
            }
        }

        private void calcSensation() {
            int calcTick = tick;
            AdvancedSensationBuilder builder = null;

            AdvancedSensationBuilderMergeOptions mergeOptions = new AdvancedSensationBuilderMergeOptions();
            mergeOptions.mode = MuscleMergeMode.MAX;

            foreach (AdvancedSensationInstance sensationInstance in playSensations.Values) {
                Sensation sensationTick = sensationInstance.getSensationAtTick(calcTick);
                if (sensationTick == null) {
                    continue;
                }

                if (builder == null) {
                    AdvancedSensationBuilderOptions options = new AdvancedSensationBuilderOptions();
                    options.streamMode = true;
                    builder = new AdvancedSensationBuilder(sensationTick, options);
                } else {
                    builder.merge(sensationTick, mergeOptions);
                }
            }
            if (builder != null) {
                // May be null due to racetime condition, on last Sensation remove
                calculatedSensation = builder.build();
            }
            Console.WriteLine("calcSensation finish step: " + calcTick);
        }

        public void playOnce(AdvancedSensationInstance instance) {
            instance.LastCalculationOfCycle += RemoveInstanceFromManager;
            addSensationInstance(instance);
        }

        public void updateSensation(String name, Sensation sensation) {
            processSensation[new AdvancedSensationInstance(name, sensation)] = ProcessState.UPDATE;
        }

        public void stopSensation(string sensationInstanceName) {
            RemoveInstanceFromManager(new AdvancedSensationInstance(sensationInstanceName));
        }

        private void RemoveInstanceFromManager(AdvancedSensationInstance instance) {
            processSensation[instance] = ProcessState.REMOVE;
        }

        public void playLoop(AdvancedSensationInstance instance) {
            addSensationInstance(instance);
        }

        private void addSensationInstance(AdvancedSensationInstance instance) {
            processSensation[instance] = ProcessState.ADD;

            if (!timer.Enabled) {
                calcManagerTick(null, null);
                timer.Start();
            }
        }

        public void stopAll() {
            resetManagerState();
            playSensations.Clear();
            processSensation.Clear();
        }

        private void resetManagerState() {
            timer.Stop();
            tick = 0;
        }

        public Dictionary<string, AdvancedSensationInstance> getPlayingSensationInstances(bool addPlanned = true) {
            Dictionary<string, AdvancedSensationInstance> returnInstances = new Dictionary<string, AdvancedSensationInstance>();
            foreach (var playInstance in playSensations) {
                returnInstances.Add(playInstance.Key, playInstance.Value);
            }
            if (addPlanned) {
                foreach (var processInstance in processSensation) {
                    if (processInstance.Value == ProcessState.ADD) {
                        returnInstances.Add(processInstance.Key.name, processInstance.Key);
                    }
                }
            }
            return returnInstances;
        }


    }
}
