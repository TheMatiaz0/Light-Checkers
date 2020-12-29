using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ConditionalAI : NotPlayer
{
	public ConditionalAI(Team team, string name) : base(team, name)
	{
	}

	protected override IEnumerator DepthAIBehaviour(List<Piece> copiedPieces)
	{
		yield return null;
	}
}
