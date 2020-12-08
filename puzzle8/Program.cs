using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace puzzle8
{
    class Program
    {
        public record Instruction(string Operation, int Value);
        
        public record State(int Accumulator, int Pointer);
        
        static void Main()
        {
            var instructions = File.ReadLines("./input.txt").Select(line =>
            {
                var split = line.Split(" ");
                return new Instruction(split[0], int.Parse(split[1]));
            }).ToArray();

            var resultPartOne = GetAccumulatorAtFirstRestart(instructions);
            var resultPartTwo = GetAccumulatorAtTermination(instructions);
            
            Console.WriteLine($"Result part one: {resultPartOne}");
            Console.WriteLine($"Result part two: {resultPartTwo}");
        }

        private static int GetAccumulatorAtFirstRestart(IReadOnlyList<Instruction> instructions)
        {
            ExecuteProgram(instructions, out var accumulator);
            return accumulator;
        }
        
        private static int GetAccumulatorAtTermination(IReadOnlyList<Instruction> instructions)
        {
            for (var i = 0; i < instructions.Count; i++)
            {
                var alteredProgram = instructions.ToArray();
                var (operation, value) = instructions[i];

                switch (operation)
                {
                    case "nop": alteredProgram[i] = new Instruction("jmp", value);
                        break;
                    case "jmp": alteredProgram[i] = new Instruction("nop", value);
                        break;
                    default:
                        continue;
                }

                if (ExecuteProgram(alteredProgram, out var accumulator))
                {
                    return accumulator;
                }
            }

            return 0;
        }

        private static bool ExecuteProgram(IReadOnlyList<Instruction> program, out int accumulator)
        {
            var state = new State(0, 0);
            var completedInstructions = new HashSet<int>();

            do
            {
                completedInstructions.Add(state.Pointer);
                state = ExecuteInstruction(program[state.Pointer], state);
            } while (!completedInstructions.Contains(state.Pointer) && state.Pointer < program.Count);

            accumulator = state.Accumulator;
            return state.Pointer == program.Count;
        }

        private static State ExecuteInstruction(Instruction instruction, State currentState)    
        {
            var (accumulator, pointer) = currentState;
            return instruction switch
            {
                {Operation: "acc"} => new State(accumulator + instruction.Value, pointer + 1),
                {Operation: "jmp"} => new State(accumulator, pointer + instruction.Value),
                {Operation: "nop"} => new State(accumulator, pointer + 1),
                _ => throw new NotImplementedException("Unknown operation")
            };
        }
    }
}
