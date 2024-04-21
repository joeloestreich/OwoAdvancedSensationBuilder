using OWOGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwoAdvancedSensationBuilder {
    internal class AdvancedSensationBuilder {

        public enum MuscleMergeMode { MAX, KEEP, OVERRIDE, MIN };

        MicroSensation micro = null;
        List<SensationWithMuscles> sensationSnippets = null;

        // Helper
        Muscle[] muscles = null;


        public AdvancedSensationBuilder(Sensation sensation) {
            analyzeSensation(sensation);
            if (sensationSnippets == null) {
                // Not null when Sensation is SensationsSequence
                this.sensationSnippets = AdvancedSensationService.splitSensation(this.micro, this.muscles);
            }
        }
        public AdvancedSensationBuilder(Sensation sensation, Muscle[] muscles) {
            this.muscles = muscles;
            analyzeSensation(sensation);
            if (sensationSnippets == null) {
                // Not null when Sensation is SensationsSequence
                this.sensationSnippets = AdvancedSensationService.splitSensation(this.micro, this.muscles);
            }
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

            int delaySnippets = (int) Math.Round(delay * 10);
            this.sensationSnippets = AdvancedSensationService.actualMerge(this.sensationSnippets, newSnippets, mode, delaySnippets);
            return this;
        }
    }
}
