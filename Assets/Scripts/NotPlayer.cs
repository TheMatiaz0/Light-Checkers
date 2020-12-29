using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public abstract class NotPlayer : Player
{
	public NotPlayer(Team team, string name) : base(team, name)
	{
	}

	public IEnumerator EnterAIBehaviour()
	{
		// copy all pieces from the board...
		List<Piece> copiedThisPlayerPieces = this.PlayerPieces.ToList();
		// List<Piece> copiedOtherPlayerPieces = GameManager.Instance.GetOtherPlayerFromCurrent().PlayerPieces.ToList();
		yield return DepthAIBehaviour(copiedThisPlayerPieces);
	}

	protected abstract IEnumerator DepthAIBehaviour(List<Piece> copiedPieces/*,  List<Piece> copiedOtherPlayerPieces */);
}
