using OWOGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OwoAdvancedSensationBuilder.AdvancedSensationBuilder;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OwoAdvancedSensationBuilder {
    internal class AdvancedSensationService {


        public static List<SensationWithMuscles> splitSensation(MicroSensation micro, Muscle[] muscles = null) {
            List<SensationWithMuscles> split = new List<SensationWithMuscles>();

            if (micro == null) {
                return split;
            }

            float time = 0.1f;

            float rampUp = micro.rampUp;
            float exitDelay = micro.Duration - micro.exitDelay;
            float rampDown = micro.Duration - micro.exitDelay - micro.rampDown;

            while (micro.Duration >= time) {
                if (rampUp >= time) {
                    float by = 1f / rampUp * time;
                    split.Add(createAdvancedMicro(lerp(0, micro.intensity, by), muscles));
                } else if (exitDelay < time) {
                    split.Add(createAdvancedMicro(0, muscles));
                } else if (rampDown < time) {
                    float by = 1f / micro.rampDown * (time - rampDown);
                    split.Add(createAdvancedMicro(lerp(micro.intensity, 0, by), muscles));
                } else {
                    split.Add(createAdvancedMicro(micro.intensity, muscles));
                }
                time = (float)Math.Round(time + 0.1f, 2);
            }

            return split;
        }

        private static int lerp(float firstFloat, float secondFloat, float by) {
            return (int)(firstFloat * (1 - by) + secondFloat * by);
        }

        private static SensationWithMuscles createAdvancedMicro(int intensity, Muscle[] muscles = null) {
            if (muscles == null || muscles.Length == 0) {
                muscles = Muscle.All;
            }

            Muscle[] modifiedMuscle = new Muscle[muscles.Length];
            for (int i = 0; i < modifiedMuscle.Length; i++) {
                Muscle m = muscles[i];
                if (intensity == 0) {
                    modifiedMuscle[i] = m.WithIntensity(0);
                } else {
                    modifiedMuscle[i] = m.WithIntensity((int)(((float)m.intensity) / 100 * intensity));
                }
            }
            Sensation s = SensationsFactory.Create(100, 0.1f, 100, 0, 0, 0);
            return new SensationWithMuscles(s, modifiedMuscle);
        }

        public static List<SensationWithMuscles> createSensationCurve(List<int> intensities, Muscle[] muscles = null) {
            List<SensationWithMuscles> curve = new List<SensationWithMuscles>();

            if (intensities == null) {
                return curve;
            }

            foreach (int intensity in intensities) {
                curve.Add(createAdvancedMicro(intensity, muscles));
            }

            return curve;
        }

        public static int float2snippets(float seconds) {
            return (int) Math.Round(seconds * 10);
        }

        public static List<SensationWithMuscles> actualMerge(List<SensationWithMuscles> origSnippets, List<SensationWithMuscles> newSnippets, MuscleMergeMode mode, int delaySnippets) {
            List<SensationWithMuscles> mergedSnippets = new List<SensationWithMuscles>();

            for (int i = 0; i < delaySnippets; i++) {
                newSnippets.Insert(0, null);
            }

            for (int i = 0; i < newSnippets.Count; i++) {
                SensationWithMuscles origSensation = null;
                if (origSnippets.Count > i) {
                    origSensation = origSnippets[i];
                }
                SensationWithMuscles newSensation = newSnippets[i];

                if (origSensation == null && newSensation == null) {
                    mergedSnippets.Add(createAdvancedMicro(0));
                } else if (origSensation == null) {
                    mergedSnippets.Add(newSensation);
                } else if (newSensation == null) {
                    mergedSnippets.Add(origSensation);
                } else { 
                    Muscle[] newMuscles = newSensation.muscles;
                    Muscle[] origMuscles = origSensation.muscles;
                    Muscle[] mergedMuscles = actualMuscleMerge(newMuscles, origMuscles, mode);
                    mergedSnippets.Add(new SensationWithMuscles(origSensation.reference, mergedMuscles));
                }
            }

            return mergedSnippets;
        }

        private static Muscle[] actualMuscleMerge(Muscle[] newMuscles, Muscle[] origMuscles, MuscleMergeMode mode) {
            switch (mode) {
                case MuscleMergeMode.MAX:
                    return actualMuscleMergeMax(newMuscles, origMuscles);
                case MuscleMergeMode.MIN:
                    return actualMuscleMergeMin(newMuscles, origMuscles);
                case MuscleMergeMode.KEEP:
                    return actualMuscleMergeKeep(newMuscles, origMuscles);
                case MuscleMergeMode.OVERRIDE:
                    return actualMuscleMergeOverride(newMuscles, origMuscles);
                default:
                    throw new Exception("Unknown Merge Type");
            }
        }

        private static Muscle[] actualMuscleMergeMax(Muscle[] newMuscles, Muscle[] origMuscles) {
            List<Muscle> mergedMuscles = new List<Muscle>();
            mergedMuscles.AddRange(origMuscles);
            foreach (Muscle m in newMuscles) {
                if (!mergedMuscles.Any(origM => origM.id == m.id)) {
                    mergedMuscles.Add(m);
                } else {
                    Muscle existing = mergedMuscles.Find(origM => origM.id == m.id);
                    if (existing.intensity < m.intensity) {
                        mergedMuscles.Remove(existing);
                        mergedMuscles.Add(m);
                    }
                }
            }
            return mergedMuscles.ToArray();
        }

        private static Muscle[] actualMuscleMergeMin(Muscle[] newMuscles, Muscle[] origMuscles) {
            List<Muscle> mergedMuscles = new List<Muscle>();
            mergedMuscles.AddRange(origMuscles);
            foreach (Muscle m in newMuscles) {
                if (!mergedMuscles.Any(origM => origM.id == m.id)) {
                    mergedMuscles.Add(m);
                } else {
                    Muscle existing = mergedMuscles.Find(origM => origM.id == m.id);
                    if (existing.intensity > m.intensity) {
                        mergedMuscles.Remove(existing);
                        mergedMuscles.Add(m);
                    }
                }
            }
            return mergedMuscles.ToArray();
        }

        private static Muscle[] actualMuscleMergeKeep(Muscle[] newMuscles, Muscle[] origMuscles) {
            List<Muscle> mergedMuscles = new List<Muscle>();
            mergedMuscles.AddRange(origMuscles);
            foreach (Muscle m in newMuscles) {
                if (!mergedMuscles.Any(origM => origM.id == m.id)) {
                    mergedMuscles.Add(m);
                }
            }
            return mergedMuscles.ToArray();
        }

        private static Muscle[] actualMuscleMergeOverride(Muscle[] newMuscles, Muscle[] origMuscles) {
            List<Muscle> mergedMuscles = new List<Muscle>();
            mergedMuscles.AddRange(newMuscles);
            foreach (Muscle m in origMuscles) {
                if (!mergedMuscles.Any(newM => newM.id == m.id)) {
                    mergedMuscles.Add(m);
                }
            }
            return mergedMuscles.ToArray();
        }

        public static List<SensationWithMuscles> cutSensation(List<SensationWithMuscles> origSnippets, int from, int till) {
            List<SensationWithMuscles> cutSnippets = new List<SensationWithMuscles>();

            for (int i = 0; i < origSnippets.Count; i++) {
                if (i >= from && i <= till) {
                    cutSnippets.Add(origSnippets[i]);
                }
            }

            return cutSnippets;
        }

    }
}
