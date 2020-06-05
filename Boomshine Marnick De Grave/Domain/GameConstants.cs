
namespace Domain {
	static public class GameConstants {
		public const float ExplodeSpeed = 60; // snelheid waarmee de radius van een bal groeit bij het exploderen, in pixelsPerSec
		public const float ImplodeSpeed = -4 * ExplodeSpeed; // snelheid waarmee de radius krimpt bij het imploderen, in pixelsPerSec
		public const int BlastRadiusMultiplier = 3; // bij een explosie groeit de bal tot 3x de initiele radius alvorens te imploderen
		public const int MinRadius = 10; // kleinst mogelijke radius van een bal
		public const int MaxRadius = 50; // grootst mogelijke radius van een bal

		public const int MaxSpeed = 200; // grootst mogelijke snelheid van een bal, in pixelsPerSec
		public const int MinSpeed = 50; // kleinst mogelijke snelheid van een bal, in pixelsPerSec
	}
}
