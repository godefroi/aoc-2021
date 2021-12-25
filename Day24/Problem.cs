﻿using Xunit;

namespace Day24;

public class Problem : ProblemBase
{

	internal static (int w, int x, int y, int z) Main(string fileName)
	{
		for (var d1 = 9; d1 > 0; d1--) {
			for (var d2 = 9; d2 > 0; d2--) {
				for (var d3 = 9; d3 > 0; d3--) {
					for (var d4 = 9; d4 > 0; d4--) {
						for (var d5 = 9; d5 > 0; d5--) {
							for (var d6 = 9; d6 > 0; d6--) {
								for (var d7 = 9; d7 > 0; d7--) {
									for (var d8 = 9; d8 > 0; d8--) {
										for (var d9 = 9; d9 > 0; d9--) {
											for (var d10 = 9; d10 > 0; d10--) {
												for (var d11 = 9; d11 > 0; d11--) {
													for (var d12 = 9; d12 > 0; d12--) {
														for (var d13 = 9; d13 > 0; d13--) {
															for (var d14 = 9; d14 > 0; d14--) {
																var ret = RunProgram(fileName, new[] { d1, d2, d3, d4, d5, d6, d7, d8, d9, d10, d11, d12, d13, d14 });
																if (ret.z == 0) {
																	Console.WriteLine($"{d1}{d2}{d3}{d4}{d5}{d6}{d7}{d8}{d9}{d10}{d11}{d12}{d13}{d14}");
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
							Console.WriteLine($"d5: {d5}");
						}
					}
				}
			}
		}

		return (0, 0, 0, 0);
		//return RunProgram(fileName, Array.Empty<int>());
	}

	private static (int w, int x, int y, int z) RunProgram(string fileName, IEnumerable<int> inputValues)
	{
		var program = File.ReadAllLines(GetFilePath(fileName)).Select(line => line.Split(' ')).ToList();
		var input   = new Queue<int>();
		var state   = new Dictionary<char, int>() {
			{ 'w', 0 },
			{ 'x', 0 },
			{ 'y', 0 },
			{ 'z', 0 },
		};

		foreach (var val in inputValues) {
			input.Enqueue(val);
		}

		foreach (var instruction in program) {
			ExecuteInstruction(instruction, input, state);
		}

		//Console.WriteLine(string.Join(',', state.Select(kvp => $"{kvp.Key}={kvp.Value}")));

		return (state['w'], state['x'], state['y'], state['z']);
	}

	private static void ExecuteInstruction(string[] instruction, Queue<int> input, Dictionary<char, int> state)
	{
		var dest = instruction[1][0];

		switch (instruction[0]) {
			case "inp":
				state[dest] = input.Dequeue();
				break;

			case "add":
				if (int.TryParse(instruction[2], out int aval)) {
					state[dest] = state[dest] + aval;
				} else {
					state[dest] = state[dest] + state[instruction[2][0]];
				}
				break;

			case "mul":
				if (int.TryParse(instruction[2], out int mval)) {
					state[dest] = state[dest] * mval;
				} else {
					state[dest] = state[dest] * state[instruction[2][0]];
				}
				break;

			case "div":
				if (int.TryParse(instruction[2], out int dval)) {
					state[dest] = state[dest] / dval;
				} else {
					state[dest] = state[dest] / state[instruction[2][0]];
				}
				break;

			case "mod":
				if (int.TryParse(instruction[2], out int oval)) {
					state[dest] = state[dest] % oval;
				} else {
					state[dest] = state[dest] % state[instruction[2][0]];
				}
				break;

			case "eql":
				if (int.TryParse(instruction[2], out int eval)) {
					state[dest] = state[dest] == eval ? 1 : 0;
				} else {
					state[dest] = state[dest] == state[instruction[2][0]] ? 1 : 0;
				}
				break;
		}
	}

	public static IEnumerable<object[]> TestData => new[] {
		new object[] { "input_trivial_multiply.txt",   new[] { 1 }, (0, -1, 0, 0) },
		new object[] { "input_three_times_larger.txt", new[] { 5, 15 } , (0, 15, 0, 1) },
		new object[] { "input_three_times_larger.txt", new[] { 5, 16 } , (0, 16, 0, 0) },
	};

	[Theory]
	[MemberData(nameof(TestData))]
	public void SampleProgramsFunctionCorrectly(string fileName, int[] input, (int w, int x, int y, int z) output)
	{
		var ret = RunProgram(fileName, input);

		Assert.Equal(output, ret);
	}
}
