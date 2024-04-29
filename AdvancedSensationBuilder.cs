using OWOGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OwoAdvancedSensationBuilder.AdvancedSensationBuilderMergeOptions;

namespace OwoAdvancedSensationBuilder {
    internal class AdvancedSensationBuilder {

        List<SensationWithMuscles> sensationSnippets = null;
        AdvancedSensationBuilderOptions options = null;

        public AdvancedSensationBuilder(Sensation sensation, AdvancedSensationBuilderOptions options = null) {
            if (options == null) {
                options = new AdvancedSensationBuilderOptions();
            }
            this.options = options;

            MicroSensation micro = analyzeSensation(sensation);
            if (sensationSnippets == null) {
                // Not null when Sensation is SensationsSequence
                sensationSnippets = AdvancedSensationService.splitSensation(micro, this.options);
            }
        }

        public AdvancedSensationBuilder(List<int> intensities, AdvancedSensationBuilderOptions options = null) {
            sensationSnippets = AdvancedSensationService.createSensationCurve(100, intensities, options);
        }

        private MicroSensation analyzeSensation(Sensation sensation) {
            if (sensation is MicroSensation) {
                return sensation as MicroSensation;
            } else if (sensation is SensationWithMuscles) {
                SensationWithMuscles withMuscles = sensation as SensationWithMuscles;
                if (options.muscles == null) {
                    options.muscles = withMuscles.muscles;
                }
                return analyzeSensation(withMuscles.reference);
            } else if (sensation is SensationsSequence) {
                sensationSnippets = new List<SensationWithMuscles>();
                SensationsSequence sequence = sensation as SensationsSequence;
                foreach (Sensation s in sequence.sensations) {
                    sensationSnippets.AddRange(new AdvancedSensationBuilder(s, options.copyWithoutMuscles()).getSnippets());
                }
            } else if (sensation is BakedSensation) {
                BakedSensation baked = sensation as BakedSensation;
                return analyzeSensation(baked.reference);
            }
            return null;
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

        public AdvancedSensationBuilder merge(Sensation s, AdvancedSensationBuilderMergeOptions mergeOptions) {

            List<SensationWithMuscles> newSnippets = new AdvancedSensationBuilder(s).getSnippets();
            if (newSnippets == null || newSnippets.Count == 0) {
                // noting to merge
                return this;
            }
            sensationSnippets = AdvancedSensationService.actualMerge(sensationSnippets, newSnippets, options, mergeOptions);
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
