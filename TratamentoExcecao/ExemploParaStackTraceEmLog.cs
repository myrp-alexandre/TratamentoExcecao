﻿using System;
using System.Diagnostics;

internal class ExemploParaStackTraceEmLog
{
    [STAThread]
    private static void Main(string[] args)
    {
        OpcaoParaLog_01();
        Console.ReadKey();

        Console.WriteLine();
        Console.WriteLine();

        OpcaoParaLog_02();
        Console.ReadKey();
    }

    private static void OpcaoParaLog_01()
    {
        try
        {
            MetodoPrivado();
        }
        catch (Exception ex)
        {
            var tracer = new StackTrace(ex, true);

            Console.WriteLine($" A soluction: {ex.Source}.");
            Console.WriteLine($" Apresentou o erro: {ex.Message}");
            Console.WriteLine($" No método: {tracer.GetFrame(0).GetMethod()}");
            Console.WriteLine($" Na linha: {tracer.GetFrame(0).GetFileLineNumber()}");
            Console.WriteLine($" Caminho: {tracer.GetFrame(0).GetFileName()}");
        }
    }

    private static void OpcaoParaLog_02()
    {
        var exemploParaLog = new ExemploParaStackTraceEmLog();
        try
        {
            exemploParaLog.MeuMetodoPublico();
        }
        catch (Exception ex)
        {
            Console.WriteLine($" A soluction: {ex.Source}. ");
            Console.WriteLine($" Apresentou o erro: {ex.Message} ");
            Console.WriteLine($" No seguinte caminho: ");

            var tracer = new StackTrace(ex, true);
            for (int indice = tracer.FrameCount - 1; indice >= 0; indice--)
            {
                var framer = tracer.GetFrame(indice);
                Console.WriteLine();
                Console.WriteLine($" Método: {framer.GetMethod()}");
                Console.WriteLine($" Linha do erro: {framer.GetFileLineNumber()}");
                Console.WriteLine($" Número da coluna: {framer.GetFileColumnNumber()}");
                Console.WriteLine($" Caminho: {framer.GetFileName()}");
            }
        }
    }

    private static void MetodoPrivado()
    {
        int divisor = 0;
        Console.WriteLine($"{13 / divisor}");
    }

    public void MeuMetodoPublico()
    {
        MeuMetodoProtegido();
    }

    protected void MeuMetodoProtegido()
    {
        var minhaClasseInterna = new MinhaClasseInterna();
        minhaClasseInterna.MetodoOndeOcorreuThrowsException();
    }

    private class MinhaClasseInterna
    {
        public void MetodoOndeOcorreuThrowsException()
        {
            try
            {
                throw new Exception("Um problema foi encontrado aqui.");
            }
            catch (Exception e)
            {
                //// Create a StackTrace that captures filename,
                //// line number and column information.
                //StackTrace tracer = new StackTrace(true);
                //string stackIndent = string.Empty;
                //for (int indice = 0; indice < tracer.FrameCount; indice++)
                //{
                //    // Note that at this level, there are four
                //    // stack frames, one for each method invocation.
                //    StackFrame framer = tracer.GetFrame(indice);
                //    Console.WriteLine();
                //    Console.WriteLine(stackIndent + $" Método: {framer.GetMethod()}");
                //    Console.WriteLine(stackIndent + $" Arquivo: {framer.GetFileName()}");
                //    Console.WriteLine(stackIndent + $" Número da linha: {framer.GetFileLineNumber()}");
                //    stackIndent += "  ";
                //}
                throw e;
            }
        }
    }
}