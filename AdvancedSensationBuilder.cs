using OWOGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwoAdvancedSensationBuilder {
    internal class AdvancedSensationBuilder {

        public enum MuscleMergeMode { MAX, KEEP, OVERRIDE, MIN };

        List<SensationWithMuscles> sensationSnippets = null;

        // Helper
        Muscle[] muscles = null;
        MicroSensation micro = null;

        public AdvancedSensationBuilder(Sensation sensation, Muscle[] muscles = null) {
            this.muscles = muscles;
            analyzeSensation(sensation);
            if (sensationSnippets == null) {
                // Not null when Sensation is SensationsSequence
                sensationSnippets = AdvancedSensationService.splitSensation(micro, this.muscles);
            }
        }

        public AdvancedSensationBuilder(List<int> intensities, Muscle[] muscles = null) {
            sensationSnippets = AdvancedSensationService.createSensationCurve(intensities, muscles);
        }

        private void analyzeSensation(Sensation sensation) {
            if (sensation is MicroSensation) {
                micro = sensation as MicroSensation;
            } else if (sensation is SensationWithMuscles) {
                SensationWithMuscles withMuscles = sensation as SensationWithMuscles;
                if (muscles == null) {
                    muscles = withMuscles.muscles;
                }
                analyzeSensation(withMuscles.reference);
            } else if (sensation is SensationsSequence) {
                sensationSnippets = new List<SensationWithMuscles>();
                SensationsSequence sequence = sensation as SensationsSequence;
                foreach (Sensation s in sequence.sensations) {
                    sensationSnippets.AddRange(new AdvancedSensationBuilder(s, muscles).getSnippets());
                }
            }
        }

        public Sensation build() {
            Sensation sensation = null;
            if (sensationSnippets == null) {
                return sensation;
            }

            foreach (Sensation s in sensationSnippets) {
                if (sensation == null) {
                    sensation = s;
                } else {
                    sensation = sensation.Append(s);
                }
            }

            return sensation;
        }

        public List<SensationWithMuscles> getSnippets() {
            return sensationSnippets;
        }

        public AdvancedSensationBuilder merge(Sensation s, MuscleMergeMode mode, float delay = 0) {

            List<SensationWithMuscles> newSnippets = new AdvancedSensationBuilder(s).getSnippets();
            if (newSnippets == null || newSnippets.Count == 0) {
                // noting to merge
                return this;
            }
            int delaySnippets = AdvancedSensationService.float2snippets(delay);
            sensationSnippets = AdvancedSensationService.actualMerge(sensationSnippets, newSnippets, mode, delaySnippets);
            return this;
        }

        public AdvancedSensationBuilder cutAtTime(float cutAtSecond, bool keepFirstHalf) {
            int cutAt = AdvancedSensationService.float2snippets(cutAtSecond);
            if (keepFirstHalf) {
                sensationSnippets = AdvancedSensationService.cutSensation(sensationSnippets, 0, cutAt);
            } else {
                sensationSnippets = AdvancedSensationService.cutSensation(sensationSnippets, cutAt, sensationSnippets.Count);
            }
            return this;
        }

        public AdvancedSensationBuilder cutAtPercent(int cutAtPercent, bool keepFirstHalf) {
            int cutAt = (int)Math.Round(((float)sensationSnippets.Count) / 100 * cutAtPercent);
            if (keepFirstHalf) {
                sensationSnippets = AdvancedSensationService.cutSensation(sensationSnippets, 0, cutAt);
            } else {
                sensationSnippets = AdvancedSensationService.cutSensation(sensationSnippets, cutAt, sensationSnippets.Count);
            }
            return this;
        }

        public AdvancedSensationBuilder cutBetweenTime(float fromSecond, float tillSecond) {
            sensationSnippets = AdvancedSensationService.cutSensation(sensationSnippets,
                AdvancedSensationService.float2snippets(fromSecond), AdvancedSensationService.float2snippets(tillSecond));
            return this;
        }

        public AdvancedSensationBuilder cutBetweenPercent(int fromPercent, int tillPercent) {
            sensationSnippets = AdvancedSensationService.cutSensation(sensationSnippets,
                (int) Math.Round(((float)sensationSnippets.Count) / 100 * fromPercent), (int)Math.Round (((float)sensationSnippets.Count) / 100 * tillPercent));
            return this;
        }
    }
}
