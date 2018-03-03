using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using OpenTK.Graphics.OpenGL4;

namespace GLDemo.GLib
{
    [PublicAPI]
    public static class Utility
    {
        public static void CheckForErrors2(
            [CallerFilePath] string file = "", 
            [CallerLineNumber] int line = -1,
            [CallerMemberName] string caller = "")
            => CheckForErrors($"{caller}@({file}:{line})");

        public static void CheckForErrors(string location)
        {
            ErrorCode error;
            while ((error = GL.GetError()) != ErrorCode.NoError)
                Console.WriteLine("ERROR {0} on {1}", error, location);
         }

        public static void ClearErrors()
        {
            
            while (GL.GetError() != ErrorCode.NoError)
            { }
        }

        [MustUseReturnValue]
        public static bool CheckForError(ErrorCode error)
        {
            ErrorCode err;
            while ((err = GL.GetError()) != error)
                if (err == ErrorCode.NoError)
                    return false;

            return true;

        }


        [NotNull,LinqTunnel]
        public static TOut[] ArraySelect<TIn, TOut>([NotNull] this TIn[] @this, [NotNull,InstantHandle] Func<TIn,TOut> func)
        {
            if (@this == null) throw new ArgumentNullException(nameof(@this));
            if (func == null) throw new ArgumentNullException(nameof(func));

            if (@this.Length == 0) return new TOut[0];

            var @return = new TOut[@this.Length];
            for (int i = 0; i < @return.Length; i++)
                @return[i] = func(@this[i]);

            return @return;

        }


        [NotNull, LinqTunnel]
        public static TOut[] ArraySelect<TIn, TOut>([NotNull] this TIn[] @this, [NotNull, InstantHandle] Func<TIn, int, TOut> func)
        {
            if (@this == null) throw new ArgumentNullException(nameof(@this));
            if (func == null) throw new ArgumentNullException(nameof(func));

            if (@this.Length == 0) return new TOut[0];

            var @return = new TOut[@this.Length];
            for (int i = 0; i < @return.Length; i++)
                @return[i] = func(@this[i], i);

            return @return;

        }
    }
}