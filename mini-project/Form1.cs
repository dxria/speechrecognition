using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace mini_project
{
    public partial class Form1 : Form
    {
        private string[] friends = { "", "", "" };
        private string[] cities = { "", "", "" };
        private string[] occupations = { "", "", "" };

        private void btn_builtin_Click(object sender, EventArgs e)
        {
            BuiltinValues();
        }
        private void btn_completetask_Click(object sender, EventArgs e)
        {
            CompleteTask();
        }

        private void btn_solve_Click(object sender, EventArgs e)
        {
            Solve();
        }


        private (string, string, string)[] GenerateCombinations()
        {
            List<(string, string, string)> combinations = new List<(string, string, string)>();

            foreach (string friend in friends)
            {
                foreach (string city in cities)
                {
                    foreach (string occupation in occupations)
                    {
                        combinations.Add((friend, city, occupation));
                    }
                }
            }

            return combinations.ToArray();
        }

        private void BuiltinValues()
        {
            tb_friend1.Text = "John";
            tb_friend2.Text = "Sam";
            tb_friend3.Text = "Michael";
            tb_city1.Text = "New Vasylky";
            tb_city2.Text = "Old York";
            tb_city3.Text = "New Liverpool";
            tb_occupation1.Text = "Dealer";
            tb_occupation2.Text = "Broker";
            tb_occupation3.Text = "Hacker";     
        }
        private void CompleteTask()
        { 
            if (!string.IsNullOrEmpty(tb_friend1.Text) && !string.IsNullOrEmpty(tb_friend2.Text) && !string.IsNullOrEmpty(tb_friend3.Text)
                && !string.IsNullOrEmpty(tb_city1.Text) && !string.IsNullOrEmpty(tb_city2.Text) && !string.IsNullOrEmpty(tb_city3.Text)
                && !string.IsNullOrEmpty(tb_occupation1.Text) && !string.IsNullOrEmpty(tb_occupation2.Text) && !string.IsNullOrEmpty(tb_occupation3.Text))
            {
                friends[0] = tb_friend1.Text;
                friends[1] = tb_friend2.Text;
                friends[2] = tb_friend3.Text;
                cities[0] = tb_city1.Text;
                cities[1] = tb_city2.Text;
                cities[2] = tb_city3.Text;
                occupations[0] = tb_occupation1.Text;
                occupations[1] = tb_occupation2.Text;
                occupations[2] = tb_occupation3.Text;
                lbl_task.Text = $"Once upon a time, best friends lived in the city of Manchester-Yorke: {friends[0]}, {friends[1]} and {friends[2]}. With times life scattered them upon different corners of the world: to {cities[0]}, to {cities[1]} and to {cities[2]}. One of them became a {occupations[0]}, the other became a {occupations[1]}, the third is a {occupations[2]}. The following is known about them: {friends[0]} doesn't live in {cities[0]}, and {friends[1]} doesn't live in {cities[2]}. The one, who lives in {cities[0]}, is not a {occupations[0]}. The one from {cities[2]} is a {occupations[2]}. {friends[1]} is not a {occupations[1]}. So what does {friends[0]} do and where does he live?";
            }
            else
            {
                MessageBox.Show("Text fields can't be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Solve()
        {
            var combinations = GenerateCombinations();

            foreach (var combination in combinations)
            {
                var friend = combination.Item1;
                var city = combination.Item2;
                var occupation = combination.Item3;

                if (friend == friends[0] && city != cities[0] && occupation != occupations[0] && (city == cities[2] && occupation == occupations[2]))
                {
                    var friend1City = city;
                    var friend1Occupation = occupation;

                    var friend2Occupation = occupations.FirstOrDefault(o => o != friend1Occupation && o != occupations[1]);
                    var friend2City = cities.FirstOrDefault(c => c != friend1City && c != cities[0]);

                    var friend3City = cities.FirstOrDefault(c => c != friend1City && c != friend2City);
                    var friend3Occupation = occupations.FirstOrDefault(o => o != friend1Occupation && o != friend2Occupation);

                    MessageBox.Show($"{friends[0]} is a {friend1Occupation} and lives in {friend1City}." +
                                    $"\n{friends[1]} is a {friend2Occupation} and lives in {friend2City}." +
                                    $"\n{friends[2]} is a {friend3Occupation} and lives in {friend3City}.",
                                    "Result", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    break;
                }
            }
        }

    }
}
