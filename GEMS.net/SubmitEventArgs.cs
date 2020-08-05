using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEMS.net
{
    class SubmitEventArgs : EventArgs
    {
        public string AudioFileName { get; set; }
        public int Wonder { get; set; }
        public int Transcendence { get; set; }
        public int Power { get; set; }
        public int Tenderness { get; set; }
        public int Nostalgia { get; set; }
        public int Peacefulness { get; set; }
        public int JoyfulActivation { get; set; }
        public int Sadness { get; set; }
        public int Tension { get; set; }

        public int Arousal { get; set; }
        public int Valence { get; set; }
        public int Tense { get; set; }
        public int Like { get; set; }
    }
}
