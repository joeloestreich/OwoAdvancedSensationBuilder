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
                this.sensationSnippets = AdvancedSensationService.splitSensation(this.micro, this.muscles);
            }
        }

        public AdvancedSensationBuilder(List<int> intensities, Muscle[] muscles = null) {
            this.sensationSnippets = AdvancedSensationService.createSensationCurve(intensities, muscles);
        }

        private void analyzeSensation(Sensation sensation) {
            if (sensation is MicroSensation) {
                this.micro = sensation as MicroSensation;
            } else if (sensation is SensationWithMuscles) {
                SensationWithMuscles withMuscles = sensation as SensationWithMuscles;
                if (this.muscles == null) {
                    this.muscles = withMuscles.muscles;
                }
                analyzeSensation(withMuscles.reference);
            } else if (sensation is SensationsSequence) {
                this.sensationSnippets = new List<SensationWithMuscles>();
                SensationsSequence sequence = sensation as SensationsSequence;
                foreach (Sensation s in sequence.sensations) {
                    sensationSnippets.AddRange(new AdvancedSensationBuilder(s, this.muscles).getSnippets());
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
            return this.sensationSnippets;
        }

        public AdvancedSensationBuilder merge(Sensation s, MuscleMergeMode mode, float delay = 0) {

            List<SensationWithMuscles> newSnippets = new AdvancedSensationBuilder(s).getSnippets();
            if (newSnippets == null || newSnippets.Count == 0) {
                // noting to merge
                return this;
            }
            int delaySnippets = AdvancedSensationService.float2snippets(delay);
            this.sensationSnippets = AdvancedSensationService.actualMerge(this.sensationSnippets, newSnippets, mode, delaySnippets);
            return this;
        }

        public AdvancedSensationBuilder cutAtTime(float cutAtSecond, bool keepFirstHalf) {
            int cutAt = AdvancedSensationService.float2snippets(cutAtSecond);
            if (keepFirstHalf) {
                this.sensationSnippets = AdvancedSensationService.cutSensation(this.sensationSnippets, 0, cutAt);
            } else {
                this.sensationSnippets = AdvancedSensationService.cutSensation(this.sensationSnippets, cutAt, this.sensationSnippets.Count);
            }
            return this;
        }

        public AdvancedSensationBuilder cutAtPercent(int cutAtPercent, bool keepFirstHalf) {
            int cutAt = (int)Math.Round(((float)this.sensationSnippets.Count) / 100 * cutAtPercent);
            if (keepFirstHalf) {
                this.sensationSnippets = AdvancedSensationService.cutSensation(this.sensationSnippets, 0, cutAt);
            } else {
                this.sensationSnippets = AdvancedSensationService.cutSensation(this.sensationSnippets, cutAt, this.sensationSnippets.Count);
            }
            return this;
        }

        public AdvancedSensationBuilder cutBetweenTime(float fromSecond, float tillSecond) {
            this.sensationSnippets = AdvancedSensationService.cutSensation(this.sensationSnippets,
                AdvancedSensationService.float2snippets(fromSecond), AdvancedSensationService.float2snippets(tillSecond));
            return this;
        }

        public AdvancedSensationBuilder cutBetweenPercent(int fromPercent, int tillPercent) {
            this.sensationSnippets = AdvancedSensationService.cutSensation(this.sensationSnippets,
                (int) Math.Round(((float)this.sensationSnippets.Count) / 100 * fromPercent), (int)Math.Round (((float)this.sensationSnippets.Count) / 100 * tillPercent));
            return this;
        }
    }
}
